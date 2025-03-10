using MediatR;
using MovieManagement.Domain.Core;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Create;

public sealed class CreateMovieCommandHandler(MovieDbContext dbContext) :
    IRequestHandler<CreateMovieCommand, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(CreateMovieCommand command,
        CancellationToken cancellationToken)
    {
        var movie = Movie.Create(command.Title, command.Genre, command.ReleaseDate, command.Rating);

        await dbContext.Movies.AddAsync(movie, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var movieDto = new MovieDto(
                        movie.Id,
                        movie.Title,
                        movie.Genre,
                        movie.ReleaseDate,
                        movie.Rating
                    );

        return Result.Success(movieDto);
    }
}
