using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var data = await _branchService.GetAllBranches();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var data = await _branchService.GetBranchById(id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchCreateUpdateDTO dto)
        {
            var data = await _branchService.CreateBranch(dto);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, BranchCreateUpdateDTO dto)
        {
            var data = await _branchService.UpdateBranch(id, dto);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var data = await _branchService.DeleteBranch(id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
