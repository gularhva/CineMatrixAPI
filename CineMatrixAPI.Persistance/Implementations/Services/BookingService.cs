using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.BookingDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookingService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Booking> bookingRepo, IGenericRepository<Ticket> ticketRepo, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookingRepo = bookingRepo;
            _ticketRepo = ticketRepo;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> AddBooking(int ticketId)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };
            if (ticketId <= 0) return new BadRequestObjectResult(responseModel);
            var ticket = await _ticketRepo.GetById(ticketId);
            if (ticket == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) 
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel); 
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            if (ticket.IsAvailable == false) { return new BadRequestObjectResult(responseModel); }
            Booking booking = new Booking();
            booking.TicketId= ticketId;
            booking.UserId = userId;
            booking.BookingDateTime = DateTime.Now;
            await _bookingRepo.Add(booking);
            ticket.IsAvailable = false;
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return new BadRequestObjectResult(responseModel);

            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> DeleteBooking(int id)
        {
            GenericResponseModel<bool> responseModel = new GenericResponseModel<bool>() { Data = false, StatusCode = 400 };

            if (id <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var booking = await _bookingRepo.GetById(id);
            if (booking == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            await _bookingRepo.DeleteById(id);

            var ticket = await _ticketRepo.GetById(booking.TicketId);
            ticket.IsAvailable = true;

            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllBookings()
        {
            GenericResponseModel<List<BookingGetDTO>> responseModel = new GenericResponseModel<List<BookingGetDTO>>()
            {
                Data = null,
                StatusCode = 404
            };

            var data = await _bookingRepo.GetAll().ToListAsync();
            if (data.Count == 0)
            {
                return new NotFoundObjectResult(responseModel);
            }

            var bookings = _mapper.Map<List<BookingGetDTO>>(data);
            responseModel.Data = bookings;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllBookingsByUserId(string userId)
        {
            GenericResponseModel<List<BookingGetDTO>> responseModel = new GenericResponseModel<List<BookingGetDTO>>()
            {
                Data = null,
                StatusCode = 400
            };

            if (string.IsNullOrEmpty(userId))
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _bookingRepo.GetAll().Where(x => x.UserId == userId).ToListAsync();

            if (data.Count == 0)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var bookings = _mapper.Map<List<BookingGetDTO>>(data);
            responseModel.Data = bookings;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetBookingByTicketId(int ticketId)
        {
            GenericResponseModel<BookingGetDTO> responseModel = new GenericResponseModel<BookingGetDTO>()
            {
                Data = null,
                StatusCode = 400
            };

            if (ticketId <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _bookingRepo.GetAll().FirstOrDefaultAsync(x => x.TicketId == ticketId);

            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var booking = _mapper.Map<BookingGetDTO>(data);
            responseModel.Data = booking;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            GenericResponseModel<BookingGetDTO> responseModel = new GenericResponseModel<BookingGetDTO>()
            {
                Data = null,
                StatusCode = 400
            };

            if (id <= 0)
            {
                return new BadRequestObjectResult(responseModel);
            }

            var data = await _bookingRepo.GetById(id);

            if (data == null)
            {
                return new NotFoundObjectResult(responseModel);
            }

            var booking = _mapper.Map<BookingGetDTO>(data);
            responseModel.Data = booking;
            responseModel.StatusCode = 200;

            return new OkObjectResult(responseModel);
        }

    }
}
