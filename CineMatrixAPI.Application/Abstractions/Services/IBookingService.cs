using CineMatrixAPI.Application.DTOs.BookingDTOs;
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
    public interface IBookingService
    {
        public Task<IActionResult> GetAllBookings();
        public Task<IActionResult> GetById(int id);
        public Task<IActionResult> GetBookingByTicketId(int ticketId);
        public Task<IActionResult> GetAllBookingsByUserId(string userId);
        public Task<IActionResult> AddBooking(int ticketId);
        public Task<IActionResult> DeleteBooking(int id);

    }
}
