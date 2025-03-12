using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Update;

internal sealed class UpdateWorkspaceHandler(IWorkspaceRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateWorkspaceCommand, Result<WorkspaceDto>>
{
    public async Task<Result<WorkspaceDto>> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
            return Result.NotFound();

        workspace.UpdateName(request.Name);

        await repository.UpdateAsync(workspace, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}