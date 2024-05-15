using CineMatrixAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _roleService.GetAllRoles();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await _roleService.GetRoleById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            return await _roleService.CreateRole(name);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await _roleService.DeleteRole(id);
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, string name)
        {
            return await _roleService.UpdateRole(id, name);
        }
    }
}
