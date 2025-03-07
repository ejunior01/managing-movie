using MediatR;

namespace MovieManagement.Web.Features.Movies.Commands.Delete;

public record DeleteMovieCommand(Guid Id): IRequest;
