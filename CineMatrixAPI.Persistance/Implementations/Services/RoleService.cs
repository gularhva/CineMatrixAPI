using CineMatrixAPI.Application.Abstractions.Services;
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
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> CreateRole(string name)
        {
            GenericResponseModel<bool> response = new GenericResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (name == null)
            {
                return new BadRequestObjectResult(response);
            }

            AppRole role = new AppRole();
            role.Name = name;
            role.Id = Guid.NewGuid().ToString();

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return new ObjectResult(response) { StatusCode = 500 };
            }

            response.StatusCode = 200;
            response.Data = true;

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            GenericResponseModel<bool> response = new GenericResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (id == null)
            {
                return new BadRequestObjectResult(response);
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new ObjectResult(response) { StatusCode = 404 };
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return new ObjectResult(response) { StatusCode = 500 };
            }

            response.StatusCode = 200;
            response.Data = true;

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> GetAllRoles()
        {
            GenericResponseModel<object> response = new GenericResponseModel<object>() { Data = null, StatusCode = 400 };

            var roles = await _roleManager.Roles.ToListAsync();

            if (roles == null || roles.Count == 0)
            {
                return new ObjectResult(response) { StatusCode = 404 };
            }

            response.StatusCode = 200;
            response.Data = roles;

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> GetRoleById(string id)
        {
            GenericResponseModel<object> response = new GenericResponseModel<object>() { Data = null, StatusCode = 400 };

            if (id == null)
            {
                return new BadRequestObjectResult(response);
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new ObjectResult(response) { StatusCode = 404 };
            }

            response.StatusCode = 200;
            response.Data = role;

            return new OkObjectResult(response);
        }

        public async Task<IActionResult> UpdateRole(string id, string name)
        {
            GenericResponseModel<bool> response = new GenericResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (id == null || name == null)
            {
                return new BadRequestObjectResult(response);
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new ObjectResult(response) { StatusCode = 404 };
            }

            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return new ObjectResult(response) { StatusCode = 500 };
            }

            response.StatusCode = 200;
            response.Data = true;

            return new OkObjectResult(response);
        }
    }
}
