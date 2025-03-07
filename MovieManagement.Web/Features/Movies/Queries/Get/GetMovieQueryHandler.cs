using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Queries.Get;

public sealed class GetMovieQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<GetMovieQuery, MovieDto?>
{
    public async Task<MovieDto?> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        var movie = await dbContext
                        .Movies
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == request.Id,
            cancellationToken: cancellationToken);

        if (movie == null)
        {
            return null;
        }

        return new MovieDto(movie.Id, movie.Title, movie.Genre, movie.ReleaseDate, movie.Rating);

    }
}
