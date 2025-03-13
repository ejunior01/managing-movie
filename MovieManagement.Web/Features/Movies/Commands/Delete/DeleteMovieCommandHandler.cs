using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Core.Primitives;
using MovieManagement.Web.Persistence;

namespace MovieManagement.Web.Features.Movies.Commands.Delete;

public sealed class DeleteMovieCommandHandler(MovieDbContext dbContext) :
    IRequestHandler<DeleteMovieCommand, Result>
{
    public async Task<Result> Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        var movieToDelete = await dbContext
                                    .Movies
                                    .FirstOrDefaultAsync(m => m.Id == command.Id, cancellationToken);

        if (movieToDelete is not null)
        {
            dbContext.Movies.Remove(movieToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
