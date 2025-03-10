using Modello.Application.Common.Messaging;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.List;

internal sealed class ListWorkspacesQueryHandler(IWorkspaceRepository workspaceRepository) : IQueryHandler<ListWorkspacesQuery, IEnumerable<WorkspaceDto>>
{
    public async Task<IEnumerable<WorkspaceDto>> Handle(ListWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var workspaces = await workspaceRepository.GetAllAsync(cancellationToken);
        return workspaces.Select(workspace => new WorkspaceDto(workspace.Id, workspace.Name));
    }
}