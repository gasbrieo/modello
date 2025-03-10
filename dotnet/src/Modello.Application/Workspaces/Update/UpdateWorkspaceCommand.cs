using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.Update;

public sealed record UpdateWorkspaceCommand(Guid WorkspaceId, string Name) : ICommand<WorkspaceDto>;
