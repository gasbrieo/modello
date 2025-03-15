namespace Modello.Application.Workspaces.Update;

public sealed record UpdateWorkspaceCommand(Guid Id, string Name) : ICommand<Result<WorkspaceDto>>;
