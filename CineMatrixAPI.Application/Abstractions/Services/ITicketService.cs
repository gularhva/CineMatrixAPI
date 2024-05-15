using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface ITicketService
    {
        public Task<IActionResult> GetAllTickets();
        public Task<IActionResult> GetById(int id);
        public Task<IActionResult> GetAllTicketsByShowTimeId(int showTimeId);
        public Task<IActionResult> AddTicket(TicketCreateDTO model);
        public Task<IActionResult> UpdateTicket(int id,TicketUpdateDTO model);
        public Task<IActionResult> DeleteTicket(int id);
    }
}
