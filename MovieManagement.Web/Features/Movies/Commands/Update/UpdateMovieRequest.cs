namespace MovieManagement.Web.Features.Movies.Commands.Update;

public record UpdateMovieRequest(string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);