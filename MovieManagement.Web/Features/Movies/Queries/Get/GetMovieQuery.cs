using MediatR;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Queries.Get;

public record GetMovieQuery(Guid Id) : IRequest<MovieDto>;
