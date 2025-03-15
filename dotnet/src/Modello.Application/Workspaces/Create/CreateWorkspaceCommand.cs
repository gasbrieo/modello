namespace Modello.Application.Workspaces.Create;

public sealed record CreateWorkspaceCommand(string Name) : ICommand<Result<WorkspaceDto>>;
