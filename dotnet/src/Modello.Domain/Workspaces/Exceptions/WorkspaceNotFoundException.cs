using Modello.Domain.Common.Exceptions;

namespace Modello.Domain.Workspaces.Exceptions;

public sealed class WorkspaceNotFoundException(Guid workspaceId) : NotFoundException($"The workspace with the identifier {workspaceId} was not found.");
