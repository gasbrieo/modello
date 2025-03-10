using Modello.Application.Common.Messaging;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces.Exceptions;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Update;

internal sealed class UpdateWorkspaceHandler(IWorkspaceRepository workspaceRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateWorkspaceCommand, WorkspaceDto>
{
    public async Task<WorkspaceDto> Handle(UpdateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceRepository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
        {
            throw new WorkspaceNotFoundException(request.Id);
        }

        workspace.UpdateName(request.Name);

        await workspaceRepository.UpdateAsync(workspace, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new WorkspaceDto(workspace.Id, workspace.Name);
    }
}