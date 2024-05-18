using CineMatrixAPI.Application.DTOs.ShowTimeDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations.ShowTimeValidators
{
    public class ShowTimeCreateUpdateDTOValidator : AbstractValidator<ShowTimeCreateUpdateDTO>
    {
        public ShowTimeCreateUpdateDTOValidator()
        {
            RuleFor(dto => dto.MovieId)
                .NotEmpty().WithMessage("Movie ID is required.")
                .GreaterThan(0).WithMessage("Movie ID must be greater than 0.");

            RuleFor(dto => dto.BranchId)
                .NotEmpty().WithMessage("Branch ID is required.")
                .GreaterThan(0).WithMessage("Branch ID must be greater than 0.");

            RuleFor(dto => dto.DateTime)
                .NotEmpty().WithMessage("Date and time is required.")
                .Must(BeValidDateTime).WithMessage("Invalid date and time format.");
        }

        private bool BeValidDateTime(DateTime dateTime)
        {
            return dateTime > DateTime.UtcNow; 
        }
    }
}
