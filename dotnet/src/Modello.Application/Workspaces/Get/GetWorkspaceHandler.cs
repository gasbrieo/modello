using Modello.Application.Common.Messaging;
using Modello.Domain.Workspaces.Exceptions;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Get;

internal sealed class GetWorkspaceHandler(IWorkspaceRepository workspaceRepository) : IQueryHandler<GetWorkspaceQuery, WorkspaceDto>
{
    public async Task<WorkspaceDto> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
        {
            throw new WorkspaceNotFoundException(request.Id);
        }

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}
