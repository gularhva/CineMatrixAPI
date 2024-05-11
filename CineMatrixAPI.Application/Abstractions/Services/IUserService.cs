using CineMatrixAPI.Application.DTOs.UserDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<GenericResponseModel<List<UserGetDTO>>> GetAllUsersAsync();
        public Task<GenericResponseModel<UserGetDTO>> GetById(string id);
        public Task<GenericResponseModel<CreateUserResponseDTO>> CreateAsync(UserCreateDTO userDTO);
        public Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDTO userDTO);
        public Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName);
        public Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName);
        public Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate);
    }
}
