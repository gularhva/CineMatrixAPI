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
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> AddShowTime(ShowTimeCreateUpdateDTO model)
        {
            GenericResponseModel<ShowTimeCreateUpdateDTO> responseModel = new GenericResponseModel<ShowTimeCreateUpdateDTO>()
            {
                Data = null,
                StatusCode = 400
            };

            if (model == null)
            {
                return new BadRequestObjectResult(responseModel);
            }

            ShowTime showTime = new ShowTime()
            {
                MovieId = model.MovieId,
                BranchId = model.BranchId,
                DateTime = model.DateTime
            };

            await _showTimeRepo.Add(showTime);
            var affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows == 0)
            {
                responseModel.StatusCode = 500;
                return new ObjectResult(responseModel) { StatusCode = 500 };
            }

            responseModel.StatusCode = 200;
            responseModel.Data = model;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> DeleteShowTime(int id)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            if (id <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var showTime = await _showTimeRepo.GetById(id);

            if (showTime == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            await _showTimeRepo.DeleteById(id);
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

        public async Task<IActionResult> UpdateShowTime(int id, ShowTimeCreateUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };

            if (id <= 0 || model == null)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _showTimeRepo.GetById(id);

            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            data.DateTime = model.DateTime;
            data.MovieId = model.MovieId;
            data.BranchId = model.BranchId;

            _showTimeRepo.Update(data);
            var affectedRows = await _unitOfWork.SaveAsync();

            if (affectedRows <= 0)
            {
                return new ObjectResult(responseModel) { StatusCode = 500 };
            }

            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllShowTimes()
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new GenericResponseModel<List<ShowTimeGetDTO>>()
            {
                Data = null,
                StatusCode = 404
            };

            var data = await _showTimeRepo.GetAll().ToListAsync();

            if (data.Count == 0)
            {
                return new NotFoundObjectResult(responseModel);
            }

            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllShowTimesByBranchId(int branchId)
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 404
            };

            var data = await _showTimeRepo.GetAll().Where(x => x.BranchId == branchId).ToListAsync();
            if (data.Count == 0 || data==null)
                return new NotFoundObjectResult(responseModel);
            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }
        public async Task<IActionResult> GetAllShowTimesByMovieId(int movieId)
        {
            GenericResponseModel<List<ShowTimeGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 404
            };
            var data = await _showTimeRepo.GetAll().Where(x => x.MovieId == movieId).ToListAsync();
            if (data.Count == 0)
                return new NotFoundObjectResult(responseModel);
            var showTimes = _mapper.Map<List<ShowTimeGetDTO>>(data);
            responseModel.Data = showTimes;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            GenericResponseModel<ShowTimeGetDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (id <= 0)
                return new BadRequestObjectResult( responseModel);
            var data = await _showTimeRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var showTime = _mapper.Map<ShowTimeGetDTO>(data);
            responseModel.Data = showTime;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }
    }
}
