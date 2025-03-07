using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Delete;

public sealed class DeleteMovieCommandHandler(MovieDbContext dbContext) : 
    IRequestHandler<DeleteMovieCommand>
{
    public async Task Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        var movieToDelete = await dbContext
                                    .Movies
                                    .FirstOrDefaultAsync(m => m.Id == command.Id,cancellationToken);

        if (movieToDelete != null)
        {
            dbContext.Movies.Remove(movieToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
