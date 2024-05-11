using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BranchDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var data = await _ticketService.GetAllTickets();
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var data = await _ticketService.GetById(id);
            return StatusCode(data.StatusCode, data);
        }
        [HttpGet("{showTimeId}")]
        public async Task<IActionResult> GetAllTicketsByShowTimeId(int showTimeId)
        {
            var data = await _ticketService.GetAllTicketsByShowTimeId(showTimeId);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketCreateDTO model)
        {
            var data = await _ticketService.AddTicket(model);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketUpdateDTO model)
        {
            var data = await _ticketService.UpdateTicket(id, model);
            return StatusCode(data.StatusCode, data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var data = await _ticketService.DeleteTicket(id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
