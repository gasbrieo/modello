namespace Modello.Presentation.Middlewares;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        switch (exception)
        {
            case ValidationException validationException:
                await HandleValidationException(validationException, httpContext, cancellationToken);
                break;
            default:
                await HandleException(exception, httpContext, cancellationToken);
                break;
        }

        return true;
    }

    private async Task HandleValidationException(ValidationException exception, HttpContext httpContext, CancellationToken cancellationToken)
    {
        logger.LogWarning(exception, "An validation error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new ErrorListResponse(
            httpContext.Request.Path,
            httpContext.TraceIdentifier,
            exception.Errors.Select(e => new ErrorDetail("ValidationError", e.ErrorCode, e.ErrorMessage))
        );

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
    }

    private async Task HandleException(Exception exception, HttpContext httpContext, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occurred: {exceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ErrorResponse(
            httpContext.Request.Path,
            httpContext.TraceIdentifier,
            "InternalServerError",
            "Internal Server Error",
            "An unexpected error occurred. Please, try again later."
        );

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
    }
}
