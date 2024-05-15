using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IReviewService
    {
        public Task<IActionResult> GetAllReviews();
        public Task<IActionResult> GetReviewById(int id);
        public Task<IActionResult> GetAllReviewsByUserId(string userId);
        public Task<IActionResult> GetAllReviewsByMovieId(int movieId);
        public Task<IActionResult> CreateReview(ReviewCreateDTO dto);
        public Task<IActionResult> DeleteReview(int id);
        public Task<IActionResult> UpdateReview(ReviewUpdateDTO model);
    }
}
