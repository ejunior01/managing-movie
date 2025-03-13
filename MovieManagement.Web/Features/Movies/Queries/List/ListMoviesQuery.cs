using MediatR;
using MovieManagement.Web.Common;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public record ListMoviesQuery(
    string? Title,
    string? Genre,
    double? MinRating,
    double? MaxRating,
    DateTimeOffset? ReleaseDateFrom,
    DateTimeOffset? ReleaseDateTo,
    int Page = 1,
    int PageSize = 10) : IRequest<PagedList<MovieDto>>;