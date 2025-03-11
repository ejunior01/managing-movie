using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core.Errors;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Features.Movies.DTOs;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Update;

public sealed class UpdateMovieCommandHandler(MovieDbContext dbContext) :
    IRequestHandler<UpdateMovieCommand,Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var movieToUpdate = await dbContext
                                    .Movies
                                    .FirstOrDefaultAsync(m => m.Id == command.Id,
                                    cancellationToken: cancellationToken);

        if (movieToUpdate is null)
        {
            return Result.Failure<MovieDto>(DomainErrors.Movie.NotFound);
        }

        movieToUpdate.Update(command.Title, command.Genre, command.ReleaseDate, command.Rating);

        await dbContext.SaveChangesAsync(cancellationToken);

        var movieDto = new MovieDto(movieToUpdate.Id, movieToUpdate.Title, movieToUpdate.Genre, movieToUpdate.ReleaseDate, movieToUpdate.Rating);
        return Result.Success(movieDto);
    }
}
