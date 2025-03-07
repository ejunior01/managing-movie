﻿using MediatR;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Commands.Create;

public record CreateMovieCommand(string Title,
                                string Genre,
                                DateTimeOffset ReleaseDate,
                                double Rating): IRequest<MovieDto>;