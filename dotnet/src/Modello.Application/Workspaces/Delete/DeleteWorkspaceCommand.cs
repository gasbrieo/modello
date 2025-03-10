using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.Delete;

public sealed record DeleteWorkspaceCommand(Guid WorkspaceId) : ICommand<Unit>;
