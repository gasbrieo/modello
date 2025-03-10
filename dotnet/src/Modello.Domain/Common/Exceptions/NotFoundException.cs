namespace Modello.Domain.Common.Exceptions;

public abstract class NotFoundException(string error, string detail) : Exception(error)
{
    public string Error { get; } = error;
    public string Detail { get; } = detail;
}
