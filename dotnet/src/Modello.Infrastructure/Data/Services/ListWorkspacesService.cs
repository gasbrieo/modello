using Modello.Application.Common.Pagination;
using Modello.Application.Workspaces;
using Modello.Application.Workspaces.List;
using Modello.Infrastructure.Data.Extensions;

namespace Modello.Infrastructure.Data.Services;

internal class ListWorkspacesService(AppDbContext context) : IListWorkspacesService
{
    public Task<PagedList<WorkspaceDto>> ListAsync(ListWorkspacesQuery query, CancellationToken cancellationToken = default)
    {
        return context.Workspaces
            .Select(e => new WorkspaceDto(e.Id, e.Name))
            .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
    }
}
