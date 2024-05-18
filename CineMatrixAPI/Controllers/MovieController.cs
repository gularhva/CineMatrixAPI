using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _movieService.GetAllMovies();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return await _movieService.GetById(id);
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(MovieCreateUpdateDTO dto)
    {
        return await _movieService.AddMovie(dto);
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MovieCreateUpdateDTO dto)
    {
        return await _movieService.UpdateMovie(id, dto);
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _movieService.DeleteMovie(id);
    }

    [HttpGet("[action]/{branchId}")]
    public async Task<IActionResult> GetAllMoviesByBranchId(int branchId)
    {
        return await _movieService.GetAllMoviesByBranchId(branchId);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllMoviesByShowTime(DateTime dateTime)
    {
        return await _movieService.GetAllMoviesByShowTime(dateTime);
    }
}

