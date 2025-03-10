using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.Create;

public sealed record CreateWorkspaceCommand(string Name) : ICommand<WorkspaceDto>;
