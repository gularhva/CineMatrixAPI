using CineMatrixAPI.Application.Abstractions.Services;
using CineMatrixAPI.Application.DTOs.MovieDTOs;
using CineMatrixAPI.Application.Models;
using CineMatrixAPI.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineMatrixAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var data = await _movieService.GetAllMovies();
        return StatusCode(data.StatusCode, data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var data = await _movieService.GetById(id);
        return StatusCode(data.StatusCode, data);
    }

    [HttpPost]
    public async Task<IActionResult> AddMovie(MovieCreateUpdateDTO dto)
    {
        var data = await _movieService.AddMovie(dto);
        return StatusCode(data.StatusCode, data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieCreateUpdateDTO dto)
    {
        var data = await _movieService.UpdateMovie(id, dto);
        return StatusCode(data.StatusCode, data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var data = await _movieService.DeleteMovie(id);
        return StatusCode(data.StatusCode, data);
    }

    [HttpGet("{branchId}")]
    public async Task<IActionResult> GetAllMoviesByBranchId(int branchId)
    {
        var data = await _movieService.GetAllMoviesByBranchId(branchId);
        return StatusCode(data.StatusCode, data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMoviesByShowTime(DateTime dateTime)
    {
        var data = await _movieService.GetAllMoviesByShowTime(dateTime);
        return StatusCode(data.StatusCode, data);
    }
}

