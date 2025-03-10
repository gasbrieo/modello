using Modello.Domain.Common.Exceptions;

namespace Modello.Domain.Workspaces.Exceptions;

public sealed class WorkspaceNotFoundException : BadRequestException
{
    public WorkspaceNotFoundException(Guid workspaceId) : base("Workspace not found.", $"The workspace with the identifier '{workspaceId}' was not found.")
    {
    }
}