using CineMatrixAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userNameOrEmail = "Admin", string password = "Admin!23")
        {
            return await _authService.LoginAsync(userNameOrEmail, password);

        }
        [HttpPost]
        public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
        {
            return await _authService.LoginWithRefreshTokenAsync(refreshToken);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            return await _authService.ChangePasswordAsync(email, oldPassword, newPassword);
        }
        [Authorize(AuthenticationSchemes ="Bearer",Roles = "User,Admin")]
        [HttpPut]
        public async Task<IActionResult> LogOut()
        {
            return await _authService.LogOut();
        }
    }
}
