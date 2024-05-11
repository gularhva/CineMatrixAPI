using CineMatrixAPI.Application.DTOs.MovieDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Validations
{
    public class MovieCreateUpdateDTOValidator : AbstractValidator<MovieCreateUpdateDTO>
    {
        public MovieCreateUpdateDTOValidator()
        {
            RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title cannot be empty.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(dto => dto.Genre)
                .NotEmpty().WithMessage("Genre cannot be empty.")
                .MaximumLength(50).WithMessage("Genre cannot exceed 50 characters.");

            RuleFor(dto => dto.Language)
                .NotEmpty().WithMessage("Language cannot be empty.")
                .MaximumLength(50).WithMessage("Language cannot exceed 50 characters.");

            RuleFor(dto => dto.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0.");

            RuleFor(dto => dto.Synopsis)
                .NotEmpty().WithMessage("Synopsis cannot be empty.");

            RuleFor(dto => dto.PosterUrl)
                .NotEmpty().WithMessage("Poster URL cannot be empty.")
                .MaximumLength(255).WithMessage("Poster URL cannot exceed 255 characters.")
                .Matches(@"\b(?:https?://|www\.)\S+\b").WithMessage("Invalid Poster URL format.");

            RuleFor(dto => dto.TrailerUrl)
                .NotEmpty().WithMessage("Trailer URL cannot be empty.")
                .MaximumLength(255).WithMessage("Trailer URL cannot exceed 255 characters.")
                .Matches(@"\b(?:https?://|www\.)\S+\b").WithMessage("Invalid Trailer URL format.");

            RuleFor(dto => dto.IMDbRating)
                .InclusiveBetween(0, 10).WithMessage("IMDb rating must be between 0 and 10.");

            RuleFor(dto => dto.Country)
                .NotEmpty().WithMessage("Country cannot be empty.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(dto => dto.IsActive).NotNull().WithMessage("IsActive must be provided.");
        }
    }
}
