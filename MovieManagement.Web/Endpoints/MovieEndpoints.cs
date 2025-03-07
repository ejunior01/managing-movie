using MediatR;
using MovieManagement.Web.Features.Movies.Commands.Create;
using MovieManagement.Web.Features.Movies.Commands.Delete;
using MovieManagement.Web.Features.Movies.Commands.Update;
using MovieManagement.Web.Features.Movies.Queries.Get;
using MovieManagement.Web.Features.Movies.Queries.List;

namespace MovieManagement.Web.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
    {
        var movieApi = routes.MapGroup("/api/movies").WithTags("Movies");

        movieApi.MapPost("/", async (CreateMovieCommand command, ISender sender) =>
        {
            var movie = await sender.Send(command);
            return TypedResults.Created($"/api/movies/{movie.Id}", movie);
        });

        movieApi.MapGet("/", async (ISender sender) =>
        {
            var movies = await sender.Send(new ListMoviesQuery());
            return TypedResults.Ok(movies);
        });

        movieApi.MapGet("/{id}", async (Guid id, ISender sender) =>
        {
            var movie = await sender.Send(new GetMovieQuery(id));

            return movie is null
                ? (IResult)TypedResults.NotFound(new { Message = $"Movie with ID {id} not found." })
                : TypedResults.Ok(movie);
        });

        movieApi.MapPut("/{id}", async (Guid id, UpdateMovieRequest request, ISender sender) =>
        {

            var command = new UpdateMovieCommand(id,
                                                request.Title,
                                                request.Genre,
                                                request.ReleaseDate,
                                                request.Rating);

            try
            {
                await sender.Send(command);
                return TypedResults.NoContent();
            }
            catch (ArgumentNullException)
            {
                return (IResult)TypedResults.NotFound(new { Message = $"Movie with ID {id} not found." });
            }

        });

        movieApi.MapDelete("/{id}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new DeleteMovieCommand(id));
            return TypedResults.NoContent();
        });
    }
}