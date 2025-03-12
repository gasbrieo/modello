using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Get;

internal sealed class GetWorkspaceHandler(IWorkspaceRepository repository) : IQueryHandler<GetWorkspaceQuery, Result<WorkspaceDto>>
{
    public async Task<Result<WorkspaceDto>> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        var workspace = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
            return Result.NotFound();

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}
