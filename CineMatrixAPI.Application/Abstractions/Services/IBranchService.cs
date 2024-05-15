using CineMatrixAPI.Application.DTOs.BranchDTOs;
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
    public interface IBranchService
    {
        public Task<IActionResult> GetAllBranches();
        public Task<IActionResult> GetBranchById(int id);
        public Task<IActionResult> CreateBranch(BranchCreateUpdateDTO model);
        public Task<IActionResult> DeleteBranch(int id);
        public Task<IActionResult> UpdateBranch(int id,BranchCreateUpdateDTO model);
    }
}
