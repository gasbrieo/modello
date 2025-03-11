namespace Modello.Presentation.Responses;

public class ErrorItem(string error, string detail)
{
    public string Error { get; } = error;
    public string Detail { get; } = detail;
}
