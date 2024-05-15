using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BookingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [Authorize(AuthenticationSchemes ="Bearer",Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _bookingService.GetAllBookings();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _bookingService.GetById(id);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllBookingsByUserId(string userId)
        {
            return await _bookingService.GetAllBookingsByUserId(userId);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetBookingByTicketId(int ticketId)
        {
            return await _bookingService.GetBookingByTicketId(ticketId);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpPost("{ticketId}")]
        public async Task<IActionResult> Create(int ticketId)
        {
            return await _bookingService.AddBooking(ticketId);
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _bookingService.DeleteBooking(id);
        }
    }
}
