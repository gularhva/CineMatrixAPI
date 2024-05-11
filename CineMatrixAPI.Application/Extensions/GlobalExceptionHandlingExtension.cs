using CineMatrixAPI.Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Extensions
{
    public static class GlobalExceptionHandlingExtension
    {
        public static void ConfigureExtensionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    Console.WriteLine("Ishe dushdu!!!");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong:{contextFeature.Error}");
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal server error:{contextFeature.Error}"
                        }));
                    }
                });
            });
        }
    }
}
