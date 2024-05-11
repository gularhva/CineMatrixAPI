using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IRepositories.IEntityRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Contexts;
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
    public class ShowTimeService : IShowTimeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ShowTime> _showTimeRepo;
        public ShowTimeService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<ShowTime> showTimeRepo)
        {
            _unitOfWork = unitOfWork;
            _showTimeRepo = showTimeRepo;
            _mapper = mapper;
        }

        public async Task<GenericResponseModel<ShowTimeCreateDTO>> AddShowTime(ShowTimeCreateDTO model)
        {
            GenericResponseModel<ShowTimeCreateDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (model == null)
                return responseModel;
            ShowTime showTime = new ShowTime();
            showTime.MovieId = model.MovieId;
            showTime.BranchId = model.BranchId;
            showTime.DateTime = model.DateTime;
            await _showTimeRepo.Add(showTime);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return responseModel;
            }
            responseModel.StatusCode = 200;
            responseModel.Data = model;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteShowTime(int id)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (id <= 0)
                return responseModel;
            var showTime = await _showTimeRepo.GetById(id);
            if (showTime == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            await _showTimeRepo.DeleteById(id);
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

        public async Task<GenericResponseModel<bool>> UpdateShowTime(int id, ShowTimeUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (id <= 0 || model == null)
                return responseModel;
            var data = await _showTimeRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            data.DateTime = model.DateTime;
            data.MovieId= model.MovieId;
            data.BranchId= model.BranchId;
            _showTimeRepo.Update(data);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                return responseModel;
            }
            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimes()
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 404
            };
            var data = await _showTimeRepo.GetAll().ToListAsync();
            if (data.Count == 0)
                return responseModel;
            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimesByBranchId(int branchId)
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            var data = await _showTimeRepo.GetAll().Where(x => x.BranchId == branchId).ToListAsync();
            if (data.Count == 0)
                return responseModel;
            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimesByMovieId(int movieId)
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            var data = await _showTimeRepo.GetAll().Where(x => x.MovieId == movieId).ToListAsync();
            if (data.Count == 0)
                return responseModel;
            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<ShowTimeGetDTO>> GetById(int id)
        {
            GenericResponseModel<ShowTimeGetDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (id <= 0)
                return responseModel;
            var data = await _showTimeRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var showTime = _mapper.Map<ShowTimeGetDTO>(data);
            responseModel.Data = showTime;
            responseModel.StatusCode = 200;
            return responseModel;
        }
    }
}
