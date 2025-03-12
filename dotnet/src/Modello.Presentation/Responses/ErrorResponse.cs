namespace Modello.Presentation.Responses;

public class ErrorResponse
{
    public string Instance { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
    public IEnumerable<string> Errors { get; set; } = [];

    public static ErrorResponse FromContext(HttpContext httpContext)
    {
        return new ErrorResponse()
        {
            Instance = httpContext.Request.Path,
            TraceId = httpContext.TraceIdentifier
        };
    }

    public ErrorResponse SetErrors(params IEnumerable<string> errors)
    {
        Errors = errors;
        return this;
    }
}
