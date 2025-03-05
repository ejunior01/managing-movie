﻿using MovieManagement.Web.DTOs;

namespace MovieManagement.Web.Services;

public interface IMovieService
{
    Task<MovieDto> CreateMovieAsync(CreateMovieDto command);
    Task<MovieDto?> GetMovieByIdAsync(Guid id);
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
    Task UpdateMovieAsync(Guid id, UpdateMovieDto command);
    Task DeleteMovieAsync(Guid id);
}