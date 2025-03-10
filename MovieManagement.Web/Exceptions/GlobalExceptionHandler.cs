using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Domain.Core.Exceptions;

namespace MovieManagement.Web.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                                Exception exception,
                                                CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Instance = httpContext.Request.Path
        };

        if (exception is FluentValidation.ValidationException fluentException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            List<string> validationErrors = [];

            foreach (var error in fluentException.Errors)
            {
                validationErrors.Add(error.ErrorMessage);
            }

            problemDetails.Extensions.Add("errors", validationErrors);

        }
        else if (exception is DomainException domainException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            problemDetails.Extensions.Add("errors", domainException.Message);
        }
        else if (exception is BadHttpRequestException badHttpRequestException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            problemDetails.Extensions.Add("errors", badHttpRequestException.Message);
        }

        else
        {
            problemDetails.Title = exception.Message;
            logger.LogError("{httpContext} {exception}", httpContext, exception);
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}