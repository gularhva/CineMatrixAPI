using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BookingDTOs;
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
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var data = await _bookingService.GetAllBookings();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var data = await _bookingService.GetById(id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllBookingsByUserId(string userId)
        {
            var data = await _bookingService.GetAllBookingsByUserId(userId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetBookingByTicketId(int ticketId)
        {
            var data = await _bookingService.GetBookingByTicketId(ticketId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingCreateDTO dto)
        {
            var data = await _bookingService.AddBooking(dto);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var data = await _bookingService.DeleteBooking(id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
