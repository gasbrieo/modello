namespace Modello.Application.Workspaces.Get;

public sealed record GetWorkspaceQuery(Guid Id) : IQuery<Result<WorkspaceDto>>;
