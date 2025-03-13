using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core;
using MovieManagement.Web.Common;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;
using System.Linq.Expressions;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public sealed class ListMoviesQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<ListMoviesQuery, PagedList<MovieDto>>
{
    public async Task<PagedList<MovieDto>> Handle(ListMoviesQuery query,
        CancellationToken cancellationToken)
    {

        var moviesQuery = dbContext.Movies.AsQueryable();

        moviesQuery = ApplyFilters(query, moviesQuery);
        var totalCount = await moviesQuery.CountAsync(cancellationToken: cancellationToken);
        moviesQuery = ApplySorting(query, moviesQuery);

        var movies = await moviesQuery
                    .Select(m => new MovieDto(m.Id, m.Title, m.Genre, m.ReleaseDate, m.Rating))
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToListAsync(cancellationToken);


        return new PagedList<MovieDto>(movies, query.Page, query.PageSize, totalCount);
    }

    private static IQueryable<Movie> ApplySorting(ListMoviesQuery query, IQueryable<Movie> moviesQuery)
    {
        if (query.SortOrder?.ToLower() == "desc")
        {
            moviesQuery = moviesQuery.OrderByDescending(GetSortProperty(query));
        }
        else
        {
            moviesQuery = moviesQuery.OrderBy(GetSortProperty(query));
        }

        return moviesQuery;
    }

    private static IQueryable<Movie> ApplyFilters(ListMoviesQuery query, IQueryable<Movie> moviesQuery)
    {
        if (!string.IsNullOrWhiteSpace(query.Title))
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

        return moviesQuery;
    }

    private static Expression<Func<Movie, object>> GetSortProperty(ListMoviesQuery query)
    {
        return query.SortColumn?.ToLower() switch
        {
            "title" => movie => movie.Title,
            "genre" => movie => movie.Genre,
            "releasedate" => movie => movie.ReleaseDate,
            "rating" => movie => movie.Rating,
            _ => movie => movie.Title
        };
    }

}
