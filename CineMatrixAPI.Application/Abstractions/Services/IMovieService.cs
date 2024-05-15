using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IMovieService
    {
        public Task<IActionResult> GetAllMovies();
        public Task<IActionResult> GetById(int id);
        public Task<IActionResult> GetAllMoviesByBranchId(int branchId);
        public Task<IActionResult> GetAllMoviesByShowTime(DateTime dateTime);
        public Task<IActionResult> AddMovie(MovieCreateUpdateDTO model);
        public Task<IActionResult> UpdateMovie(int id,MovieCreateUpdateDTO model);
        public Task<IActionResult> DeleteMovie(int id);
    }
}
