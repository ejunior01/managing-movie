using MediatR;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public record ListMoviesQuery : IRequest<Result<List<MovieDto>>>;