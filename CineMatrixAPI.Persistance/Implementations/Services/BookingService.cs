using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BookingDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Booking> _bookingRepo;
        private readonly IGenericRepository<Ticket> _ticketRepo;
        private readonly UserManager<AppUser> _userManager;
        public BookingService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Booking> bookingRepo, IGenericRepository<Ticket> ticketRepo, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookingRepo = bookingRepo;
            _ticketRepo = ticketRepo;
            _userManager = userManager;
        }

        public async Task<GenericResponseModel<BookingCreateDTO>> AddBooking(BookingCreateDTO model)
        {
            GenericResponseModel<BookingCreateDTO> responseModel = new GenericResponseModel<BookingCreateDTO>()
            {
                Data = null,
                StatusCode = 400
            };
            if (model == null)
            {
                return responseModel;
            }
            var ticket = await _ticketRepo.GetById(model.TicketId);
            //var data = await _bookingRepo.GetAll().Include(x=>x.Ticket).FirstOrDefaultAsync(x=>x.TicketId==model.TicketId);
            if (ticket == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            if (ticket.IsAvailable == false) { return responseModel; }
            Booking booking = new Booking();
            booking = _mapper.Map<Booking>(model);
            booking.BookingDateTime = DateTime.Now;
            await _bookingRepo.Add(booking);
            ticket.IsAvailable = false;
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;

            responseModel.StatusCode = 200;
            responseModel.Data = model;
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteBooking(int id)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>() { Data = false, StatusCode = 400 };
            if (id <= 0)
                return responseModel;
            var booking = await _bookingRepo.GetById(id);
            if (booking == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            await _bookingRepo.DeleteById(id);
            var ticket = await _ticketRepo.GetById(booking.TicketId);
            ticket.IsAvailable = true;
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return responseModel;

            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<BookingGetDTO>>> GetAllBookings()
        {
            GenericResponseModel<List<BookingGetDTO>> responseModel = new() { Data = null, StatusCode = 404 };
            var data = await _bookingRepo.GetAll().ToListAsync();
            if (data.Count == 0)
                return responseModel;
            var bookings = _mapper.Map<List<BookingGetDTO>>(data);
            responseModel.Data = bookings;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<List<BookingGetDTO>>> GetAllBookingsByUserId(string userId)
        {
            GenericResponseModel<List<BookingGetDTO>> responseModel = new() { Data = null, StatusCode = 400 };
            if (string.IsNullOrEmpty(userId))
                return responseModel;
            var data = await _bookingRepo.GetAll().Where(x => x.UserId == userId).ToListAsync();
            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }

            var bookings = _mapper.Map<List<BookingGetDTO>>(data);
            responseModel.Data = bookings;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<BookingGetDTO>> GetBookingByTicketId(int ticketId)
        {
            GenericResponseModel<BookingGetDTO> responseModel = new() { Data = null, StatusCode = 400 };
            if (ticketId <= 0)
                return responseModel;
            var data = await _bookingRepo.GetAll().FirstOrDefaultAsync(x => x.TicketId == ticketId);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return responseModel;
            }
            var bookings = _mapper.Map<BookingGetDTO>(data);
            responseModel.Data = bookings;
            responseModel.StatusCode = 200;
            return responseModel;
        }

        public async Task<GenericResponseModel<BookingGetDTO>> GetById(int id)
        {
            GenericResponseModel<BookingGetDTO> responseModel = new() { StatusCode = 400, Data = null };
            if (id <= 0) return responseModel;
            var data = await _bookingRepo.GetById(id);
            if (data == null)
                return responseModel;
            var booking = _mapper.Map<BookingGetDTO>(data);
            responseModel.Data = booking;
            responseModel.StatusCode = 200;
            return responseModel;
        }
    }
}
