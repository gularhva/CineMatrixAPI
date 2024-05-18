using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IShowTimeService
    {
        public Task<IActionResult> GetAllShowTimes();
        public Task<IActionResult> GetById(int id);
        public Task<IActionResult> GetAllShowTimesByMovieId(int movieId);
        public Task<IActionResult> GetAllShowTimesByBranchId(int branchId);
        public Task<IActionResult> AddShowTime(ShowTimeCreateUpdateDTO model);
        public Task<IActionResult> UpdateShowTime(int id, ShowTimeCreateUpdateDTO model);
        public Task<IActionResult> DeleteShowTime(int id);
    }
}
