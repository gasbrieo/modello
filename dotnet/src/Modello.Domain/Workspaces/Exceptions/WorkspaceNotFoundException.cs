using Modello.Domain.Common.Exceptions;

namespace Modello.Domain.Workspaces.Exceptions;

public sealed class WorkspaceNotFoundException : NotFoundException
{
    public WorkspaceNotFoundException() : base("Workspace not found.", "The workspace with the provided identifier was not found.")
    {
    }
}