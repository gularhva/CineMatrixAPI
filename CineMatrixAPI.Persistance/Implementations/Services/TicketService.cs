using AutoMapper;
using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.DTOs.TicketDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Persistance.Implementations.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Ticket> _ticketRepo;
        public TicketService(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Ticket> ticketRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ticketRepo = ticketRepo;
        }
        public async Task<IActionResult> AddTicket(TicketCreateDTO model)
        {
            GenericResponseModel<TicketCreateDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (model == null)
                return new BadRequestObjectResult(responseModel);
            if (_ticketRepo.GetAll().Any(x => x.SeatNumber == model.SeatNumber && x.ShowTimeId == model.ShowTimeId))
            {
                responseModel.StatusCode = 409; // Conflict
                return new ConflictObjectResult(responseModel);
            }
            Ticket ticket = new Ticket();
            ticket = _mapper.Map<Ticket>(model);
            ticket.IsAvailable = true;
            await _ticketRepo.Add(ticket);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return new BadRequestObjectResult(responseModel);
            responseModel.StatusCode = 200;
            responseModel.Data = model;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> DeleteTicket(int id)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (id <= 0)
                return new BadRequestObjectResult(responseModel);
            var ticket = await _ticketRepo.GetById(id);
            if (ticket == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            await _ticketRepo.DeleteById(id);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows == 0)
                return new BadRequestObjectResult(responseModel);
            responseModel.StatusCode = 200;
            responseModel.Data = true;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllTickets()
        {
            GenericResponseModel<List<TicketGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 404
            };
            var data = await _ticketRepo.GetAll().ToListAsync();
            if (data.Count == 0)
                return new NotFoundObjectResult(responseModel);
            var tickets = _mapper.Map<List<TicketGetDTO>>(data);
            responseModel.Data = tickets;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetAllTicketsByShowTimeId(int showTimeId)
        {
            GenericResponseModel<List<TicketGetDTO>> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (showTimeId <= 0)
                return new BadRequestObjectResult(responseModel);
            var data = await _ticketRepo.GetAll().Where(x => x.ShowTimeId == showTimeId).ToListAsync();
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var tickets = _mapper.Map<List<TicketGetDTO>>(data);
            responseModel.Data = tickets;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            GenericResponseModel<TicketGetDTO> responseModel = new()
            {
                Data = null,
                StatusCode = 400
            };
            if (id <= 0) return new BadRequestObjectResult(responseModel);
            var data = await _ticketRepo.GetById(id);
            if (data == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }
            var ticket = _mapper.Map<TicketGetDTO>(data);
            responseModel.Data = ticket;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }

        public async Task<IActionResult> UpdateTicket(int id, TicketUpdateDTO model)
        {
            GenericResponseModel<bool> responseModel = new()
            {
                Data = false,
                StatusCode = 400
            };
            if (model == null || id <= 0)
                return new BadRequestObjectResult(responseModel);
            var ticket = await _ticketRepo.GetById(id);
            if (ticket == null)
            {
                responseModel.StatusCode = 404;
                return new NotFoundObjectResult(responseModel);
            }

            var existingTicketWithSameSeat = _ticketRepo.GetAll()
                .FirstOrDefault(x => x.SeatNumber == model.SeatNumber && x.ShowTimeId == ticket.ShowTimeId && x.Id != id);

            if (existingTicketWithSameSeat != null)
            {
                responseModel.StatusCode = 409;
                return new ConflictObjectResult(responseModel);
            }
            _mapper.Map(model, ticket);
            _ticketRepo.Update(ticket);
            var affectedRows = await _unitOfWork.SaveAsync();
            if (affectedRows <= 0)
            {
                responseModel.StatusCode = 500;
                return new ObjectResult(responseModel);
            }
            responseModel.Data = true;
            responseModel.StatusCode = 200;
            return new OkObjectResult(responseModel);
        }
    }
}
