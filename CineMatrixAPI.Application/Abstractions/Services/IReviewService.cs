using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IReviewService
    {
        public Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviews();
        public Task<GenericResponseModel<ReviewGetDTO>> GetReviewById(int id);
        public Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviewsByUserId(string userId);
        public Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviewsByMovieId(int movieId);
        public Task<GenericResponseModel<ReviewCreateDTO>> CreateReview(ReviewCreateDTO dto);
        public Task<GenericResponseModel<bool>> DeleteReview(int id);
        public Task<GenericResponseModel<bool>> UpdateReview(ReviewUpdateDTO model);
    }
}
