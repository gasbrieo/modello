using Modello.Application.Common.Messaging;
using Modello.Domain.Workspaces.Exceptions;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Get;

internal sealed class GetWorkspaceHandler(IWorkspaceRepository repository) : IQueryHandler<GetWorkspaceQuery, WorkspaceDto>
{
    public async Task<WorkspaceDto> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        var workspace = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
        {
            throw new WorkspaceNotFoundException();
        }

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}
