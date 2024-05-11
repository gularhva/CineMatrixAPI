using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IShowTimeService
    {
        public Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimes();
        public Task<GenericResponseModel<ShowTimeGetDTO>> GetById(int id);
        public Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimesByMovieId(int movieId);
        public Task<GenericResponseModel<List<ShowTimeGetDTO>>> GetAllShowTimesByBranchId(int branchId);
        public Task<GenericResponseModel<ShowTimeCreateDTO>> AddShowTime(ShowTimeCreateDTO model);
        public Task<GenericResponseModel<bool>> UpdateShowTime(int id, ShowTimeUpdateDTO model);
        public Task<GenericResponseModel<bool>> DeleteShowTime(int id);
    }
}
