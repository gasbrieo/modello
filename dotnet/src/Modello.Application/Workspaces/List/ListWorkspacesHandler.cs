using Modello.Application.Common.Messaging;
using Modello.Application.Common.Pagination;

namespace Modello.Application.Workspaces.List;

internal sealed class ListWorkspacesHandler(IListWorkspacesService service) : IQueryHandler<ListWorkspacesQuery, PagedList<WorkspaceDto>>
{
    public Task<PagedList<WorkspaceDto>> Handle(ListWorkspacesQuery request, CancellationToken cancellationToken)
    {
        return service.ListAsync(request, cancellationToken);
    }
}
