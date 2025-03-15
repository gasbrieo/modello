using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Create;

internal sealed class CreateWorkspaceHandler(IWorkspaceRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<CreateWorkspaceCommand, Result<WorkspaceDto>>
{
    public async Task<Result<WorkspaceDto>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = new Workspace(request.Name);

        await repository.AddAsync(workspace, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Created(new WorkspaceDto(workspace.Id, workspace.Name), $"/api/workspaces/{workspace.Id}");
    }
}
