using MovieManagement.Domain.Core.Primitives;

namespace MovieManagement.Domain.Core.Errors;

public static class DomainErrors
{

    public static class Movie
    {
        public static Error NotFound => new("Movie.Found", "The movie with the specified identifier was not found.");

    }

    public static class General
    {
        public static Error UnProcessableRequest => new(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }


}