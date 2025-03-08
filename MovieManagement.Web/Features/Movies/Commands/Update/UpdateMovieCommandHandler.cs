using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Update;

public sealed class UpdateMovieCommandHandler(ILogger<UpdateMovieCommandHandler> logger,MovieDbContext dbContext) :
    IRequestHandler<UpdateMovieCommand>
{
    public async Task Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
    {
        var movieToUpdate = await dbContext
                                    .Movies
                                    .FirstOrDefaultAsync(m => m.Id == command.Id,
                                    cancellationToken: cancellationToken);

        if (movieToUpdate is null)
        {
            logger.LogError("Invalid Movie Id: {id}.", command.Id);
            throw new ArgumentNullException($"Invalid Movie Id: {command.Id}.");
        }

        movieToUpdate.Update(command.Title, command.Genre, command.ReleaseDate, command.Rating);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
