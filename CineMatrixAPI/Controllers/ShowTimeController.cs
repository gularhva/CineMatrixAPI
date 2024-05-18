using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShowTimeController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;
        public ShowTimeController(IShowTimeService showTimeService)
        {
            _showTimeService = showTimeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _showTimeService.GetAllShowTimes();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _showTimeService.GetById(id);
        }
        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetAllShowTimesByBranchId(int branchId)
        {
            return await _showTimeService.GetAllShowTimesByBranchId(branchId);
        }       
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetAllShowTimesByMovieId(int movieId)
        {
            return await _showTimeService.GetAllShowTimesByMovieId(movieId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ShowTimeCreateUpdateDTO model)
        {
            return await _showTimeService.AddShowTime(model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ShowTimeCreateUpdateDTO model)
        {
            return await _showTimeService.UpdateShowTime(id, model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _showTimeService.DeleteShowTime(id);
        }

    }
}
