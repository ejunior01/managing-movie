using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public sealed class ListMoviesQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<ListMoviesQuery, Result<List<MovieDto>>>
{
    public async Task<Result<List<MovieDto>>> Handle(ListMoviesQuery query,
        CancellationToken cancellationToken)
    {
        var movies =  await dbContext
                        .Movies
                        .AsNoTracking()
                        .Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating))
                        .ToListAsync(cancellationToken);

        return Result.Success(movies);
    }
}
