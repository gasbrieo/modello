namespace Modello.Application.Workspaces.Delete;

public sealed record DeleteWorkspaceCommand(Guid Id) : ICommand<Result>;
