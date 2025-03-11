using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Common;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public sealed class ListMoviesQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<ListMoviesQuery, PagedList<MovieDto>>
{
    public async Task<PagedList<MovieDto>> Handle(ListMoviesQuery query,
        CancellationToken cancellationToken)
    {

        var moviesQuery = dbContext
                        .Movies
                        .AsNoTracking()
                        .AsQueryable();


        var totalCount = await moviesQuery.CountAsync(cancellationToken: cancellationToken);

        var movies = await moviesQuery
                        .Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating))
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .ToListAsync(cancellationToken);


        return new PagedList<MovieDto>(movies, query.Page, query.PageSize, totalCount);
    }
}
