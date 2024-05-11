using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<GenericResponseModel<object>> GetAllRoles();
        Task<GenericResponseModel<object>> GetRoleById(string id);
        Task<GenericResponseModel<bool>> CreateRole(string name);
        Task<GenericResponseModel<bool>> DeleteRole(string id);
        Task<GenericResponseModel<bool>> UpdateRole(string id, string name);
    }
}
