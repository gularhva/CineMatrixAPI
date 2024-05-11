using CineMatrixAPI.Application.DTOs.TicketDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations.TicketValidators
{
    public class TicketCreateDTOValidator : AbstractValidator<TicketCreateDTO>
    {
        public TicketCreateDTOValidator()
        {
            RuleFor(dto => dto.SeatNumber)
                .NotEmpty().WithMessage("Seat number is required.")
                .GreaterThan(0).WithMessage("Seat number must be greater than 0.");

            RuleFor(dto => dto.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(dto => dto.ShowTimeId)
                .NotEmpty().WithMessage("Show time ID is required.")
                .GreaterThan(0).WithMessage("Show time ID must be greater than 0.");
        }
    }
}
