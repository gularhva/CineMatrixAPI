using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<IActionResult> GetAllUsersAsync();
        public Task<IActionResult> GetById(string id);
        public Task<IActionResult> CreateAsync(UserCreateDTO userDTO);
        public Task<IActionResult> UpdateUserAsync(string id,UserUpdateDTO userDTO);
        public Task<IActionResult> DeleteUserAsync(string userIdOrName);
        public Task<IActionResult> GetRolesToUserAsync(string userIdOrName);
        public Task<IActionResult> AssignRoleToUserAsync(string userId, string[] roles);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate);
    }
}
