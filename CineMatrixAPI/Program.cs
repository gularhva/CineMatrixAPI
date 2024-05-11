using CineMatrixAPI.Application.Abstractions.IRepositories;
using CineMatrixAPI.Application.Abstractions.IRepositories.IEntityRepositories;
using CineMatrixAPI.Application.Abstractions.IUnitOfWorks;
using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.Profiles;
using CineMatrixAPI.Contexts;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Persistance.Implementations.Repositories;
using CineMatrixAPI.Persistance.Implementations.Repositories.EntityRepositories;
using CineMatrixAPI.Persistance.Implementations.Services;
using CineMatrixAPI.Persistance.Implementations.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System;
using Serilog.Core;
using CineMatrixAPI.Application.Extensions;
using FluentValidation.AspNetCore;
using CineMatrixAPI.Application.Validations;
using CineMatrixAPI.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<BookingCreateDTOValidator>());
//builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt => { opt.SuppressModelStateInvalidFilter = true; });
//builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.UseCustomValidationResponse();
var config = builder.Configuration.GetConnectionString("ApplicationDbContext");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(config));

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

Logger log = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Debug)
    .WriteTo.File("Logs/myJsonLogs.json")
    .WriteTo.File("logs/mylogs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

Log.Logger = log;
builder.Host.UseSerilog(log);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExtensionHandler();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
