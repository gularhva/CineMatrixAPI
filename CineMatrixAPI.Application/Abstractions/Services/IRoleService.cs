using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<IActionResult> GetAllRoles();
        Task<IActionResult> GetRoleById(string id);
        Task<IActionResult> CreateRole(string name);
        Task<IActionResult> DeleteRole(string id);
        Task<IActionResult> UpdateRole(string id, string name);
    }
}
