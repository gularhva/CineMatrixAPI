using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Review> _reviewRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Review> repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reviewRepo = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> CreateReview(ReviewCreateDTO dto)
        {
            GenericResponseModel<ReviewCreateDTO> responseModel = new GenericResponseModel<ReviewCreateDTO>() { Data = null, StatusCode = 400 };

            if (dto == null)
            {
                return new BadRequestObjectResult(responseModel);
            }
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            Review review = new Review();
            review.UserId = userId;
            review.MovieId = dto.MovieId;
            review.CreatedAt = DateTime.Now;
            review.ReviewText = dto.ReviewText;
            review.Point = dto.Point;
            await _reviewRepo.Add(review);
            var affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return new ObjectResult(responseModel) { StatusCode = 500 };
            }

            responseModel.StatusCode = 200;
            responseModel.Data = dto;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> DeleteReview(int id)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>() { StatusCode = 400, Data = false };

            if (id <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var review = await _reviewRepo.GetById(id);

            if (review == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            await _reviewRepo.DeleteById(id);
            var affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return new ObjectResult(responseModel) { StatusCode = 500 };
            }

            responseModel.StatusCode = 200;
            responseModel.Data = true;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllReviews()
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new GenericResponseModel<List<ReviewGetDTO>>() { StatusCode = 404, Data = null };

            var data = await _reviewRepo.GetAll().ToListAsync();

            if (data.Count == 0)
            {
                return new NotFoundObjectResult(responseModel);
            }

            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);

            responseModel.StatusCode = 200;
            responseModel.Data = reviews;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllReviewsByMovieId(int movieId)
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new GenericResponseModel<List<ReviewGetDTO>>()
            {
                Data = null,
                StatusCode = 400
            };

            if (movieId <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _reviewRepo.GetAll().Where(x => x.MovieId == movieId).ToListAsync();

            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);

            responseModel.StatusCode = 200;
            responseModel.Data = reviews;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllReviewsByUserId(string userId)
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new GenericResponseModel<List<ReviewGetDTO>>()
            {
                Data = null,
                StatusCode = 400
            };

            if (string.IsNullOrEmpty(userId))
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _reviewRepo.GetAll().Where(x => x.UserId == userId).ToListAsync();

            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);

            responseModel.StatusCode = 200;
            responseModel.Data = reviews;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetReviewById(int id)
        {
            GenericResponseModel<ReviewGetDTO> responseModel = new GenericResponseModel<ReviewGetDTO>()
            {
                Data = null,
                StatusCode = 400
            };

            if (id <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _reviewRepo.GetById(id);

            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var review = _mapper.Map<ReviewGetDTO>(data);

            responseModel.StatusCode = 200;
            responseModel.Data = review;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> UpdateReview(int id, ReviewUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            if (model == null)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var review = await _reviewRepo.GetById(id);

            if (review == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            _mapper.Map(model, review);
            _reviewRepo.Update(review);

            var affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows <= 0)
            {
                responseModel.StatusCode = 500;
                return new ObjectResult(responseModel) { StatusCode = 500 };
            }

            responseModel.Data = true;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

    }
}
