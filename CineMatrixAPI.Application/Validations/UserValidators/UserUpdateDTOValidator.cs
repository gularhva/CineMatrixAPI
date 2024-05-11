using CineMatrixAPI.Application.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations.UserValidators
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(dto => dto.Id)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(dto => dto.Firstname)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(dto => dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(dto => dto.Birthday)
                .NotEmpty().WithMessage("Birthday is required.")
                .Must(BeValidBirthday).WithMessage("Invalid birthday.");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");
        }

        private bool BeValidBirthday(DateTime birthday)
        {
            return birthday <= DateTime.UtcNow;
        }
    }
}
