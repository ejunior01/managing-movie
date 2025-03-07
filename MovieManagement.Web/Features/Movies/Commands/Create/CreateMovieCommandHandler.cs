using MediatR;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Models;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Create;

public sealed class CreateMovieCommandHandler(MovieDbContext dbContext) :
    IRequestHandler<CreateMovieCommand, MovieDto>
{
    public async Task<MovieDto> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = Movie.Create(command.Title, command.Genre, command.ReleaseDate, command.Rating);

        await dbContext.Movies.AddAsync(movie, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new MovieDto(
                        movie.Id,
                        movie.Title,
                        movie.Genre,
                        movie.ReleaseDate,
                        movie.Rating
                    );
    }
}
