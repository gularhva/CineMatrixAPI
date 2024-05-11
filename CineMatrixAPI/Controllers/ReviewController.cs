using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var data = await _reviewService.GetAllReviews();
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var data = await _reviewService.GetReviewById(id);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetAllReviewsByMovieId(int movieId)
        {
            var data = await _reviewService.GetAllReviewsByMovieId(movieId);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllReviewsByUserId(string userId)
        {
            var data = await _reviewService.GetAllReviewsByUserId(userId);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewCreateDTO dto)
        {
            var data = await _reviewService.CreateReview(dto);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview(ReviewUpdateDTO dto)
        {
            var data = await _reviewService.UpdateReview(dto);
            return StatusCode(data.StatusCode, data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var data = await _reviewService.DeleteReview(id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
