using AutoMapper;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> AssignRoleToUserAsync(string userId, string[] roles)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400,
            };
            if (userId == null || roles == null)
            {
                return new BadRequestObjectResult(responseModel);
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var availableRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, availableRoles);
            if (!roles.ToList().Contains("User"))
                await _userManager.AddToRoleAsync(user, "User");
            var result = await _userManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(responseModel);
            }
            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> CreateAsync(UserCreateDTO userDTO)
        {
            GenericResponseModel<CreateUserResponseDTO> responseModel = new GenericResponseModel<CreateUserResponseDTO>()
            {
                Data = null,
                StatusCode = 400,
            };
            if (userDTO == null)
            {
                return new BadRequestObjectResult(responseModel);
            }
            var id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = id,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Birthday = userDTO.Birthday,
            }, userDTO.Password);
            responseModel.Data = new CreateUserResponseDTO { Succeeded = result.Succeeded };
            responseModel.StatusCode = result.Succeeded ? 200 : 400;
            if (!result.Succeeded)
            {
                responseModel.Data.Message = string.Join("\n", result.Errors.Select(error => $"{error.Code}-{error.Description}"));
            }
            AppUser user = await _userManager.FindByNameAsync(userDTO.UserName);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
                user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.AddToRoleAsync(user, "User");
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> DeleteUserAsync(string userIdOrName)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400,
            };
            if (userIdOrName == null)
            {
                return new BadRequestObjectResult(responseModel);
            }
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(responseModel);
            }
            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            GenericResponseModel<List<UserGetDTO>> responseModel = new GenericResponseModel<List<UserGetDTO>>()
            {
                Data = null,
                StatusCode = 404
            };
            var users = await _userManager.Users.ToListAsync();
            if (users.Count == 0)
            {
                return new NotFoundObjectResult(responseModel);
            }
            var user = _mapper.Map<List<UserGetDTO>>(users);
            responseModel.Data = user;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetById(string id)
        {
            GenericResponseModel<UserGetDTO> responseModel = new GenericResponseModel<UserGetDTO>()
            {
                Data = null,
                StatusCode = 400,
            };
            if (string.IsNullOrEmpty(id))
            {
                return new BadRequestObjectResult(responseModel);
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            responseModel.Data = _mapper.Map<UserGetDTO>(user);
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetRolesToUserAsync(string userIdOrName)
        {
            GenericResponseModel<string[]> responseModel = new GenericResponseModel<string[]>()
            {
                Data = null,
                StatusCode = 400,
            };
            if (string.IsNullOrEmpty(userIdOrName))
            {
                return new BadRequestObjectResult(responseModel);
            }
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var roles = await _userManager.GetRolesAsync(user);
            string[] rolesArray = roles.ToArray();
            responseModel.Data = rolesArray;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
        {
            var userToUpdate = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userToUpdate != null)
            {
                userToUpdate.RefreshToken = refreshToken;
                userToUpdate.RefreshTokenEndTime = accessTokenDate.AddMinutes(10);
                await _userManager.UpdateAsync(userToUpdate);
            }
        }
        public async Task<IActionResult> UpdateUserAsync(UserUpdateDTO userDTO)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400,
            };
            if (userDTO == null)
                return new BadRequestObjectResult(responseModel);
            var user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user == null)
                user = await _userManager.FindByNameAsync(userDTO.Username);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            //user.Id = userDTO.Id;
            //user.FirstName = userDTO.Firstname;
            //user.LastName = userDTO.LastName;
            //user.Email = userDTO.Email;
            //user.UserName = userDTO.Username;
            //user.Birthday = userDTO.Birthday;
            _mapper.Map(userDTO, user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                responseModel.StatusCode = 200;
                responseModel.Data = true;
            }
            return new OkObjectResult(responseModel);
        }
    }
}
