using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _userService.GetAllUsersAsync();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await _userService.GetById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDTO userDTO)
        {
            return await _userService.CreateAsync(userDTO);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDTO userDTO)
        {
            return await _userService.UpdateUserAsync(userDTO);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpDelete("{userIdOrName}")]
        public async Task<IActionResult> Delete(string userIdOrName)
        {
            return await _userService.DeleteUserAsync(userIdOrName);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{userIdOrName}")]
        public async Task<IActionResult> GetRolesToUser(string userIdOrName)
        {
            return await _userService.GetRolesToUserAsync(userIdOrName);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("{userId}")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] roles)
        {
            return await _userService.AssignRoleToUserAsync(userId, roles);
        }

    }
}
