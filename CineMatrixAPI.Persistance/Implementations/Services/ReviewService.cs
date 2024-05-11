using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Review> _reviewRepo;
        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Review> repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reviewRepo = repository;
        }
        public async Task<GenericResponseModel<ReviewCreateDTO>> CreateReview(ReviewCreateDTO dto)
        {
            GenericResponseModel<ReviewCreateDTO> responseModel = new() { Data = null, StatusCode = 400 };
            if (dto == null)
                return responseModel;
            Review review = new();
            review = _mapper.Map<Review>(dto);
            await _reviewRepo.Add(review);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return responseModel;
            }
            responseModel.StatusCode = 200;
            responseModel.Data = dto;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteReview(int id)
        {
            GenericResponseModel<bool> responseModel = new() { StatusCode = 400, Data = false };
            if (id <= 0) { return responseModel; }
            var review = await _reviewRepo.GetById(id);
            if (review == null) 
            { 
                responseModel.StatusCode = 404;
                return responseModel; 
            }
            await _reviewRepo.DeleteById(id);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return responseModel;
            }
            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviews()
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new() { StatusCode = 404, Data = null };
            var data = await _reviewRepo.GetAll().ToListAsync();
            if(data.Count == 0)
                return responseModel;
            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);
            responseModel.StatusCode = 200;
            responseModel.Data = reviews;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviewsByMovieId(int movieId)
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new() { Data=null,StatusCode= 400};
            if(movieId <= 0)
            {
                return responseModel;
            }
            var data = await _reviewRepo.GetAll().Where(x=>x.MovieId==movieId).ToListAsync();
            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);
            responseModel.StatusCode = 200;
            responseModel.Data = reviews;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ReviewGetDTO>>> GetAllReviewsByUserId(string userId)
        {
            GenericResponseModel<List<ReviewGetDTO>> responseModel = new() { Data = null, StatusCode = 400 };
            if (string.IsNullOrEmpty(userId))
                return responseModel;
            var data = await _reviewRepo.GetAll().Where(x => x.UserId == userId).ToListAsync();
            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var reviews = _mapper.Map<List<ReviewGetDTO>>(data);
            responseModel.StatusCode = 200;
            responseModel.Data = reviews;
            return responseModel;
        }

        public async Task<GenericResponseModel<ReviewGetDTO>> GetReviewById(int id)
        {
            GenericResponseModel<ReviewGetDTO> responseModel = new() { StatusCode = 400, Data = null };
            if(id<=0)
            {
                return responseModel;
            }
            var data = await _reviewRepo.GetById(id);
            if(data==null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var review = _mapper.Map<ReviewGetDTO>(data);
            responseModel.StatusCode = 200;
            responseModel.Data = review;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> UpdateReview(ReviewUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new() { StatusCode = 400, Data = false };
            if (model == null)
                return responseModel;
            var review = await _reviewRepo.GetById(model.Id);
            if (review == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            _mapper.Map(model,review);
            _reviewRepo.Update(review);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                responseModel.StatusCode = 500;
                return responseModel;
            }
            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return responseModel;
        }
    }
}
