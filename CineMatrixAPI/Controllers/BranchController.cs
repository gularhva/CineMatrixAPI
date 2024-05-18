using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _branchService.GetAllBranches();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _branchService.GetBranchById(id);
        }

        [Authorize(AuthenticationSchemes ="Bearer",Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BranchCreateUpdateDTO dto)
        {
            return await _branchService.CreateBranch(dto);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BranchCreateUpdateDTO dto)
        {
            return await _branchService.UpdateBranch(id, dto);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _branchService.DeleteBranch(id);
        }
    }
}
