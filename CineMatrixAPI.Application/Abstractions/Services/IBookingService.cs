using CineMatrixAPI.Application.DTOs.BookingDTOs;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions.Services
{
    public interface IBookingService
    {
        public Task<GenericResponseModel<List<BookingGetDTO>>> GetAllBookings();
        public Task<GenericResponseModel<BookingGetDTO>> GetById(int id);
        public Task<GenericResponseModel<BookingGetDTO>> GetBookingByTicketId(int ticketId);
        public Task<GenericResponseModel<List<BookingGetDTO>>> GetAllBookingsByUserId(string userId);
        public Task<GenericResponseModel<BookingCreateDTO>> AddBooking(BookingCreateDTO model);
        public Task<GenericResponseModel<bool>> DeleteBooking(int id);

    }
}
