using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IMovieService
    {
        public Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMovies();
        public Task<GenericResponseModel<MovieGetDTO>> GetById(int id);
        public Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMoviesByBranchId(int branchId);
        public Task<GenericResponseModel<List<MovieGetDTO>>> GetAllMoviesByShowTime(DateTime dateTime);
        public Task<GenericResponseModel<MovieCreateUpdateDTO>> AddMovie(MovieCreateUpdateDTO model);
        public Task<GenericResponseModel<bool>> UpdateMovie(int id,MovieCreateUpdateDTO model);
        public Task<GenericResponseModel<bool>> DeleteMovie(int id);
        //todo janra dile gore get eleme metodlarin yaz!!
    }
}
