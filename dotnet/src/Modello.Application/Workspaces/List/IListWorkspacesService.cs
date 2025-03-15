namespace Modello.Application.Workspaces.List;

public interface IListWorkspacesService
{
    Task<PagedList<WorkspaceDto>> ListAsync(ListWorkspacesQuery query, CancellationToken cancellationToken = default);
}