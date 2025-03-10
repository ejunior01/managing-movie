using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core.Errors;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Queries.Get;

public sealed class GetMovieQueryHandler(MovieDbContext dbContext) :
    IRequestHandler<GetMovieQuery, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(GetMovieQuery request,
        CancellationToken cancellationToken)
    {
        var movie = await dbContext
                        .Movies
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == request.Id,
            cancellationToken: cancellationToken);

        if (movie is null)
        {
            return Result.Failure<MovieDto>(DomainErrors.Movie.NotFound);
        }


        var movieDto = new MovieDto(movie.Id, movie.Title, movie.Genre, movie.ReleaseDate, movie.Rating);

        return Result.Success(movieDto);

    }
}
