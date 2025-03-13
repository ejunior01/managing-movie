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

        if(!string.IsNullOrWhiteSpace(query.Title))
        {
            moviesQuery = moviesQuery.Where(m => m.Title.Contains(query.Title));
        }

        if (!string.IsNullOrWhiteSpace(query.Genre))
        {
            moviesQuery = moviesQuery.Where(m => m.Genre.Contains(query.Genre));
        }

        if (query.MinRating.HasValue)
        {
            moviesQuery = moviesQuery.Where(m => m.Rating >= query.MinRating);
        }

        if (query.MaxRating.HasValue)
        {
            moviesQuery = moviesQuery.Where(m => m.Rating <= query.MaxRating);
        }

        if (query.ReleaseDateFrom.HasValue)
        {
            moviesQuery = moviesQuery.Where(m => m.ReleaseDate >= query.ReleaseDateFrom);
        }

        if (query.ReleaseDateTo.HasValue)
        {
            moviesQuery = moviesQuery.Where(m => m.ReleaseDate <= query.ReleaseDateTo);
        }
             
        var totalCount = await moviesQuery.CountAsync(cancellationToken: cancellationToken);

        var movies = await moviesQuery
                        .Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating))
                        .Skip((query.Page - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .ToListAsync(cancellationToken);


        return new PagedList<MovieDto>(movies, query.Page, query.PageSize, totalCount);
    }
}
