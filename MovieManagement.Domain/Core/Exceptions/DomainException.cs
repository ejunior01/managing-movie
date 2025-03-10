using MovieManagement.Domain.Core.Primitives;

namespace MovieManagement.Domain.Core.Exceptions;

public class DomainException(Error error) : Exception(error.Message)
{
    public Error Error { get; } = error;
}