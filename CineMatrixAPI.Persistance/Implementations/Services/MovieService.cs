using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Movie> _movieRepo;
        public MovieService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Movie> movieRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _movieRepo = movieRepo;
        }
        public async Task<GenericResponseModel<MovieCreateUpdateDTO>> AddMovie(MovieCreateUpdateDTO model)
        {
            GenericResponseModel<MovieCreateUpdateDTO> responseModel = new GenericResponseModel<MovieCreateUpdateDTO>()
            {
                Data = null,
                StatusCode = 400,
            };
            if (model == null)
                return responseModel;
            var data = await _movieRepo.GetAll().FirstOrDefaultAsync(x=>x.Title.Trim() == model.Title);
            if (data != null)
            {
                return responseModel;
            }
            Movie movie = new Movie();
            movie = _mapper.Map<Movie>(model);
            movie.CreatedTime = DateTime.Now;
            await _movieRepo.Add(movie);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;
            responseModel.StatusCode = 200;
            responseModel.Data = model;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteMovie(int id)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 404
            };
            if (id <= 0)
                return responseModel;
            var movie = await _movieRepo.GetById(id);
            if (movie == null)
                return responseModel;
            await _movieRepo.DeleteById(id);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;
            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return responseModel;

        }

        public async Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMovies()
        {
            GenericResponseModel<List<MovieGetDTO>> responseModel = new GenericResponseModel<List<MovieGetDTO>>()
            {
                Data = null,
                StatusCode = 404
            };
            var data = await _movieRepo.GetAll().ToListAsync();
            if (data.Count == 0)
                return responseModel;
            List<MovieGetDTO> movies = _mapper.Map<List<MovieGetDTO>>(data);
            responseModel.Data = movies;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMoviesByBranchId(int branchId)
        {
            GenericResponseModel<List<MovieGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 404
            };

            var moviesInBranch = await _movieRepo.GetAll()
                .Where(movie => movie.ShowTimes.Any(showTime => showTime.BranchId == branchId))
                .ToListAsync();

            if (moviesInBranch == null || !moviesInBranch.Any())
                return responseModel;

            var data = _mapper.Map<List<MovieGetDTO>>(moviesInBranch);
            if (data.Count == 0)
                return responseModel;

            responseModel.Data = data;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMoviesByShowTime(DateTime dateTime)
        {
            GenericResponseModel<List<MovieGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            var data = await _movieRepo.GetAll()
            .Include(x => x.ShowTimes)
            .Where(movie => movie.ShowTimes.Any(st => st.DateTime == dateTime))
            .ToListAsync();
            if (data.Count == 0)
                return responseModel;
            List<MovieGetDTO> movies = _mapper.Map<List<MovieGetDTO>>(data);
            responseModel.Data = movies;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<MovieGetDTO>> GetById(int id)
        {
            GenericResponseModel<MovieGetDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (id < 0)
                return responseModel;
            var data = await _movieRepo.GetById(id);
            if (data == null) return responseModel;
            var user = _mapper.Map<MovieGetDTO>(data);
            responseModel.Data = user;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> UpdateMovie(int id, MovieCreateUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (model == null)
                return responseModel;
            var data = await _movieRepo.GetById(id);
            if (data == null)
                return responseModel;
            _mapper.Map(model, data);
            _movieRepo.Update(data);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                return responseModel;
            }
            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return responseModel;
        }
    }
}
