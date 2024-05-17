using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAll()
        {
            return await _reviewService.GetAllReviews();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _reviewService.GetReviewById(id);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetAllReviewsByMovieId(int movieId)
        {
            return await _reviewService.GetAllReviewsByMovieId(movieId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllReviewsByUserId(string userId)
        {
            return await _reviewService.GetAllReviewsByUserId(userId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateDTO dto)
        {
            return await _reviewService.CreateReview(dto);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ReviewUpdateDTO dto)
        {
            return await _reviewService.UpdateReview(id,dto);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _reviewService.DeleteReview(id);
        }
    }
}
