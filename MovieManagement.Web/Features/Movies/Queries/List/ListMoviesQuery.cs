using MediatR;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public record ListMoviesQuery : IRequest<List<MovieDto>>;