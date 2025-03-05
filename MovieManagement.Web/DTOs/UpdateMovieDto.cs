﻿namespace MovieManagement.Web.DTOs;

public record UpdateMovieDto(string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);