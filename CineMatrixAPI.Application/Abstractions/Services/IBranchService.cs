using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.ReviewDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IBranchService
    {
        public Task<GenericResponseModel<List<BranchGetDTO>>> GetAllBranches();
        public Task<GenericResponseModel<BranchGetDTO>> GetBranchById(int id);
        public Task<GenericResponseModel<BranchCreateUpdateDTO>> CreateBranch(BranchCreateUpdateDTO model);
        public Task<GenericResponseModel<bool>> DeleteBranch(int id);
        public Task<GenericResponseModel<bool>> UpdateBranch(int id,BranchCreateUpdateDTO model);
    }
}
