using Modello.Domain.Common.Exceptions;

namespace Modello.Presentation.Middlewares;

public class ErrorItem(string error, string detail)
{
    public string Error { get; } = error;
    public string Detail { get; } = detail;
}

public class ErrorResponse(string instance, string traceId)
{
    public string Instance { get; } = instance;
    public string TraceId { get; } = traceId;
    public ICollection<ErrorItem> Errors { get; } = [];

    public static ErrorResponse FromContext(HttpContext httpContext)
    {
        return new(httpContext.Request.Path, httpContext.TraceIdentifier);
    }

    public ErrorResponse AddError(ErrorItem error)
    {
        Errors.Add(error);
        return this;
    }
}


public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, ProblemDetailsFactory problemDetailsFactory) : IExceptionHandler
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
