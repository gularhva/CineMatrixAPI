using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Application.Models;
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
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetAllUsersAsync();
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var data = await _userService.GetById(id);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO userDTO)
        {
            var data = await _userService.CreateAsync(userDTO);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userDTO)
        {
            var data = await _userService.UpdateUserAsync(userDTO);
            return StatusCode(data.StatusCode, data);
        }
        [HttpDelete("{userIdOrName}")]
        public async Task<IActionResult> DeleteUser(string userIdOrName)
        {
            var data = await _userService.DeleteUserAsync(userIdOrName);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{userIdOrName}")]
        public async Task<IActionResult> GetRolesToUser(string userIdOrName)
        {
            var data = await _userService.GetRolesToUserAsync(userIdOrName);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] roles)
        {
            var data = await _userService.AssignRoleToUserAsync(userId, roles);
            return StatusCode(data.StatusCode, data);
        }

    }
}
