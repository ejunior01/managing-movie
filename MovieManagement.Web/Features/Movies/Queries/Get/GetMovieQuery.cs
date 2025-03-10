using MediatR;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Queries.Get;

public record GetMovieQuery(Guid Id) : IRequest<Result<MovieDto>>;
