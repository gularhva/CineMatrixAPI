using CineMatrixAPI.Application.DTOs.BookingDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations
{
    public class BookingCreateDTOValidator:AbstractValidator<BookingCreateDTO>
    {
        public BookingCreateDTOValidator()
        {
            RuleFor(dto => dto.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty.")
            .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters.");

            RuleFor(dto => dto.TicketId)
                .NotEmpty().WithMessage("Ticket ID cannot be empty.")
                .GreaterThan(0).WithMessage("Ticket ID must be greater than 0.");
        }
    }
}
