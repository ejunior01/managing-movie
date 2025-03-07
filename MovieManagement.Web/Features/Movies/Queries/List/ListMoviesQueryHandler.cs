using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public sealed class ListMoviesQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<ListMoviesQuery, List<MovieDto>>
{
    public async Task<List<MovieDto>> Handle(ListMoviesQuery query, CancellationToken cancellationToken)
    {
        return await dbContext
                        .Movies
                        .AsNoTracking()
                        .Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating))
                        .ToListAsync(cancellationToken);
    }
}
