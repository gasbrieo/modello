namespace Modello.Presentation.Responses;

public class ErrorResponse
{
    public string Instance { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
    public ICollection<ErrorItem> Errors { get; set; } = [];

    public static ErrorResponse FromContext(HttpContext httpContext)
    {
        return new ErrorResponse()
        {
            Instance = httpContext.Request.Path,
            TraceId = httpContext.TraceIdentifier
        };
    }

    public ErrorResponse AddError(ErrorItem error)
    {
        Errors.Add(error);
        return this;
    }
}
