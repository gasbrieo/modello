namespace Modello.Application.Workspaces.List;

public sealed record ListWorkspacesQuery(int PageNumber, int PageSize) : IQuery<Result<PagedList<WorkspaceDto>>>;
