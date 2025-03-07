using MediatR;
using MovieManagement.Web.Features.Movies.DTOs;

namespace MovieManagement.Web.Features.Movies.Commands.Update;

public record UpdateMovieCommand(Guid Id,
                                string Title,
                                string Genre,
                                DateTimeOffset ReleaseDate,
                                double Rating): IRequest;