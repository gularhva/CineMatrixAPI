using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Branch> _branchRepo;
        public BranchService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Branch> branchRepo)
        {
            _unitOfWork = unitOfWork;
            _branchRepo = branchRepo;
            _mapper = mapper;
        }
        public async Task<GenericResponseModel<BranchCreateUpdateDTO>> CreateBranch(BranchCreateUpdateDTO model)
        {
            GenericResponseModel<BranchCreateUpdateDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (model == null)
                return responseModel;
            Branch branch = new Branch();
            branch.Name = model.Name;
            branch.Location = model.Location;
            await _branchRepo.Add(branch);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;
            responseModel.StatusCode = 200;
            responseModel.Data = model;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteBranch(int id)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (id <= 0)
                return responseModel;
            var branch = await _branchRepo.GetById(id);
            if (branch == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            await _branchRepo.DeleteById(id);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;
            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<BranchGetDTO>>> GetAllBranches()
        {
            GenericResponseModel<List<BranchGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            var data = await _branchRepo.GetAll().ToListAsync();
            if (data.Count == 0)
                return responseModel;
            List<BranchGetDTO> branches = _mapper.Map<List<BranchGetDTO>>(data);
            responseModel.Data = branches;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<BranchGetDTO>> GetBranchById(int id)
        {
            GenericResponseModel<BranchGetDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (id <= 0)
                return responseModel;
            var data = await _branchRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var branch = _mapper.Map<BranchGetDTO>(data);
            responseModel.Data = branch;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> UpdateBranch(int id, BranchCreateUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (id <= 0 || model == null)
                return responseModel;
            var data = await _branchRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            _mapper.Map(model, data);
            _branchRepo.Update(data);
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
