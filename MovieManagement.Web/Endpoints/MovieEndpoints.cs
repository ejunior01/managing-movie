using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Web.Features.Movies.Commands.Create;
using MovieManagement.Web.Features.Movies.Commands.Delete;
using MovieManagement.Web.Features.Movies.Commands.Update;
using MovieManagement.Web.Features.Movies.Queries.Get;
using MovieManagement.Web.Features.Movies.Queries.List;
using MovieManagement.Web.Notifications;

namespace MovieManagement.Web.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
    {
        var movieApi = routes.MapGroup("/api/movies").WithTags("Movies");

        movieApi.MapPost("/", async (CreateMovieCommand command, IMediator mediatr) =>
        {
            var result = await mediatr.Send(command);

            await mediatr.Publish(new MovieCreatedNotification(result.Value.Id));

            return TypedResults.Created($"/api/movies/{result.Value.Id}", result.Value);
        });

        movieApi.MapGet("/", async (ISender sender, [FromQuery] int page = 1, [FromQuery] int pageSize = 10) =>
        {
            var query = new ListMoviesQuery(page, pageSize);
            var result = await sender.Send(query);
            return TypedResults.Ok(result);
        });

        movieApi.MapGet("/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetMovieQuery(id));

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : (IResult)TypedResults.NotFound(result.Error.Message);
        });

        movieApi.MapPut("/{id}", async (Guid id, UpdateMovieRequest request, ISender sender) =>
        {

            var command = new UpdateMovieCommand(id,
                                                request.Title,
                                                request.Genre,
                                                request.ReleaseDate,
                                                request.Rating);

            var result = await sender.Send(command);

            return result.IsSuccess
                ? TypedResults.NoContent()
                : (IResult)TypedResults.NotFound(result.Error.Message);

        });

        movieApi.MapDelete("/{id}", async (Guid id, ISender sender) =>
        {
            await sender.Send(new DeleteMovieCommand(id));
            return TypedResults.NoContent();
        });
    }
}