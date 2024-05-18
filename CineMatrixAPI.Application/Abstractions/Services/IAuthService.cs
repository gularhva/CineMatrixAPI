using CineMatrixAPI.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        public Task<IActionResult> LoginAsync(string userNameOrEmail, string password);
        public Task<IActionResult> LoginWithRefreshTokenAsync(string refreshToken);
        public Task<IActionResult> LogOut();
        public Task<IActionResult> ChangePasswordAsync(string oldPassword, string newPassword);
    }
}
