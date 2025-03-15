namespace Modello.Application.Workspaces.List;

internal sealed class ListWorkspacesHandler(IListWorkspacesService service) : IQueryHandler<ListWorkspacesQuery, Result<PagedList<WorkspaceDto>>>
{
    public async Task<Result<PagedList<WorkspaceDto>>> Handle(ListWorkspacesQuery request, CancellationToken cancellationToken)
    {
        return await service.ListAsync(request, cancellationToken);
    }
}
