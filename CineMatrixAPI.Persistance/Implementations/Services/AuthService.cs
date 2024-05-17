using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.Abstractions;
using CineMatrixAPI.Application.DTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            GenericResponseModel<bool> response = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(oldPassword) && string.IsNullOrEmpty(newPassword))
                return new BadRequestObjectResult(response);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.StatusCode = 404;
                return new NotFoundObjectResult(response);
            }
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                response.StatusCode = 500;
                return new ObjectResult(response);
            }
            response.StatusCode = 200;
            response.Data = true;
            return new OkObjectResult(response);
        }
        public async Task<IActionResult> LoginAsync(string userNameOrEmail, string password)
        {
            GenericResponseModel<TokenDTO> response = new GenericResponseModel<TokenDTO>()
            {
                Data = null,
                StatusCode = 400
            };
            if (string.IsNullOrEmpty(userNameOrEmail) || string.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult(response);
            }

            var user = await _userManager.FindByNameAsync(userNameOrEmail) ?? await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                response.StatusCode = 401; // Unauthorized
                return new UnauthorizedObjectResult(response);
            }

            var accessTokenResult = await _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(accessTokenResult.RefreshToken, user, accessTokenResult.Expiration);

            if (accessTokenResult == null)
            {
                response.StatusCode = 500; // Internal server error
                return new ObjectResult(response);
            }

            response.Data = accessTokenResult;
            response.StatusCode = 200; // OK
            return new OkObjectResult(response);
        }
        public async Task<IActionResult> LoginWithRefreshTokenAsync(string refreshToken)// todo adini deyisdir.
        {
            GenericResponseModel<TokenDTO> response = new GenericResponseModel<TokenDTO>()
            {
                Data = null,
                StatusCode = 400
            };
            if (!string.IsNullOrEmpty(refreshToken))
            {
                return new BadRequestObjectResult(response);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user == null && user.RefreshTokenEndTime <= DateTime.UtcNow)
            {
                response.StatusCode = 401;
                return new UnauthorizedObjectResult(response);
            }
            //await _signInManager.SignInAsync(user, isPersistent: false);
            var accessTokenResult = await _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(accessTokenResult.RefreshToken, user, accessTokenResult.Expiration);

            if (accessTokenResult == null)
            {
                response.StatusCode = 500; // Internal server error
                return new ObjectResult(response);
            }

            response.Data = accessTokenResult;
            response.StatusCode = 200; // OK
            return new OkObjectResult(response);
        }
        public async Task<IActionResult> LogOut()
        {
            GenericResponseModel<bool> response = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return new BadRequestObjectResult(response);
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.StatusCode = 404; 
                return new NotFoundObjectResult(response);
            }
            user.RefreshToken = null;
            user.RefreshTokenEndTime = null;
            var result = await _userManager.UpdateAsync(user);
            await _signInManager.SignOutAsync();
            if (!result.Succeeded)
            {
                response.StatusCode = 500;
                return new ObjectResult(response);
            }
            response.Data = true;
            response.StatusCode = 200;
            return new OkObjectResult(response);
        }
    }
}
