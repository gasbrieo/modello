using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.Update;

public sealed record UpdateWorkspaceCommand(Guid Id, string Name) : ICommand<WorkspaceDto>;
