using CineMatrixAPI.Application.DTOs.BranchDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations
{
    public class BranchCreateUpdateDTOValidator:AbstractValidator<BranchCreateUpdateDTO>
    {
        public BranchCreateUpdateDTOValidator()
        {
            RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(dto => dto.Location)
                .NotEmpty().WithMessage("Location cannot be empty.")
                .MaximumLength(100).WithMessage("Location cannot exceed 100 characters.");
        }
    }
}
