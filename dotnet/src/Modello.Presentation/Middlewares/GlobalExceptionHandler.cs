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
            _ => HandleException(exception, httpContext)
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private static ErrorResponse HandleValidationException(ValidationException exception, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = ErrorResponse.FromContext(httpContext);
        response.SetErrors(exception.Errors.Select(e => e.ErrorCode));

        return response;
    }

    private ErrorResponse HandleException(Exception exception, HttpContext httpContext)
    {
        logger.LogError(exception, "An unexpected error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = ErrorResponse.FromContext(httpContext);
        response.SetErrors("Internal Server Error");

        return response;
    }
}
