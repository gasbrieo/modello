using Modello.Domain.Common.Exceptions;

namespace Modello.Domain.Workspaces.Exceptions;

public sealed class WorkspaceNameEmptyException : BadRequestException
{
    public WorkspaceNameEmptyException() : base("Name must not be empty.", "The name of the workspace cannot be empty or contain only white spaces.")
    {
    }
}