using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CineMatrixAPI.Application.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                    .SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList());

                    var response = new GenericResponseModel<ErrorDto> { Data = errorDto, StatusCode = 400 };
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
