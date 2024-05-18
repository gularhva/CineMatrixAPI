using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _ticketService.GetAllTickets();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _ticketService.GetById(id);
        }
        [HttpGet("[action]/{showTimeId}")]
        public async Task<IActionResult> GetAllTicketsByShowTimeId(int showTimeId)
        {
            return await _ticketService.GetAllTicketsByShowTimeId(showTimeId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TicketCreateDTO model)
        {
            return await _ticketService.AddTicket(model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TicketUpdateDTO model)
        {
            return await _ticketService.UpdateTicket(id, model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _ticketService.DeleteTicket(id);
        }
    }
}
