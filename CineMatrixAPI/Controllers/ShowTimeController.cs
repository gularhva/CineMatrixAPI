using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
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
        public async Task<IActionResult> GetAllShowTimes()
        {
            var data = await _showTimeService.GetAllShowTimes();
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowTimeById(int id)
        {
            var data = await _showTimeService.GetById(id);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetAllShowTimesByBranchId(int branchId)
        {
            var data = await _showTimeService.GetAllShowTimesByBranchId(branchId);
            return StatusCode(data.StatusCode, data);
        }       
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetAllShowTimesByMovieId(int movieId)
        {
            var data = await _showTimeService.GetAllShowTimesByMovieId(movieId);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> AddShowTime(ShowTimeCreateDTO model)
        {
            var data = await _showTimeService.AddShowTime(model);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowTime(int id, ShowTimeUpdateDTO model)
        {
            var data = await _showTimeService.UpdateShowTime(id, model);
            return StatusCode(data.StatusCode, data);
        } 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            var data = await _showTimeService.DeleteShowTime(id);
            return StatusCode(data.StatusCode, data);
        }

    }
}
