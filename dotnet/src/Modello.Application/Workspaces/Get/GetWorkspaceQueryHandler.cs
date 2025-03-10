using Modello.Application.Common.Messaging;
using Modello.Domain.Workspaces.Exceptions;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Get;

internal sealed class GetWorkspaceQueryHandler(IWorkspaceRepository workspaceRepository) : IQueryHandler<GetWorkspaceQuery, WorkspaceDto>
{
    public async Task<WorkspaceDto> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

        if (workspace is null)
        {
            throw new WorkspaceNotFoundException(request.WorkspaceId);
        }

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}
