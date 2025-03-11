using Modello.Domain.Common.Exceptions;
using Modello.Presentation.Responses;

namespace Modello.Presentation.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        var response = exception switch
        {
            ValidationException validationException => HandleValidationException(validationException, httpContext),
            BadRequestException badRequestException => HandleBadRequestException(badRequestException, httpContext),
            NotFoundException notFoundException => HandleNotFoundException(notFoundException, httpContext),
            _ => HandleException(exception, httpContext)
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private static ErrorResponse HandleValidationException(ValidationException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = ErrorResponse.FromContext(httpContext);

        foreach (var error in exception.Errors)
        {
            response.AddError(new ErrorItem(error.ErrorCode, error.ErrorMessage));
        }

        return response;
    }

    private static ErrorResponse HandleBadRequestException(BadRequestException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = ErrorResponse.FromContext(httpContext);
        response.AddError(new ErrorItem(exception.Error, exception.Detail));

        return response;
    }

    private static ErrorResponse HandleNotFoundException(NotFoundException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        var response = ErrorResponse.FromContext(httpContext);
        response.AddError(new ErrorItem(exception.Error, exception.Detail));

        return response;
    }

    private ErrorResponse HandleException(Exception exception, HttpContext httpContext)
    {
        logger.LogError(exception, "An unexpected error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = ErrorResponse.FromContext(httpContext);
        response.AddError(new ErrorItem("Internal Server Error", "An unexpected error occurred. Please, try again later."));

        return response;
    }
}
