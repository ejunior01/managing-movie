using MediatR;
using MovieManagement.Domain.Core.Primitives;

namespace MovieManagement.Web.Features.Movies.Commands.Delete;

public record DeleteMovieCommand(Guid Id) : IRequest<Result>;
