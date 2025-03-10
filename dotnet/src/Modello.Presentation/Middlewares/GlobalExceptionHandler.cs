using Modello.Domain.Common.Exceptions;

namespace Modello.Presentation.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, ProblemDetailsFactory problemDetailsFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        var response = exception switch
        {
            ValidationException validationException => HandleValidationException(validationException, httpContext),
            NotFoundException notFoundException => HandleNotFoundException(notFoundException, httpContext),
            _ => HandleException(exception, httpContext)
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private ProblemDetails HandleValidationException(ValidationException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            httpContext,
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            detail: "One or more validation errors occurred.",
            instance: httpContext.Request.Path
        );

        problemDetails.Extensions["errors"] = exception.Errors.Select(error => error.ErrorMessage);

        return problemDetails;
    }

    private ProblemDetails HandleNotFoundException(NotFoundException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            httpContext,
            statusCode: StatusCodes.Status404NotFound,
            title: "Not Found",
            detail: exception.Message,
            instance: httpContext.Request.Path
        );

        return problemDetails;
    }

    private ProblemDetails HandleException(Exception exception, HttpContext httpContext)
    {
        logger.LogError(exception, "An unexpected error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            httpContext,
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Internal Server Error",
            detail: "An unexpected error occurred. Please, try again later.",
            instance: httpContext.Request.Path
        );

        return problemDetails;
    }
}
