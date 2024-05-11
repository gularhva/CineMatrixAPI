using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface ITicketService
    {
        public Task<GenericResponseModel<List<TicketGetDTO>>> GetAllTickets();
        public Task<GenericResponseModel<TicketGetDTO>> GetById(int id);
        public Task<GenericResponseModel<List<TicketGetDTO>>> GetAllTicketsByShowTimeId(int showTimeId);
        public Task<GenericResponseModel<TicketCreateDTO>> AddTicket(TicketCreateDTO model);
        public Task<GenericResponseModel<bool>> UpdateTicket(int id,TicketUpdateDTO model);
        public Task<GenericResponseModel<bool>> DeleteTicket(int id);
    }
}
