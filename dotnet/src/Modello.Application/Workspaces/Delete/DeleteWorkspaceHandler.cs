using Modello.Application.Common.Messaging;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces.Events;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Delete;

internal sealed class DeleteWorkspaceHandler(IWorkspaceRepository repository, IUnitOfWork unitOfWork, IMediator mediator) : ICommandHandler<DeleteWorkspaceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is null)
        {
            return Unit.Value;
        }

        await repository.DeleteAsync(workspace, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await mediator.Publish(new WorkspaceDeletedEvent(workspace.Id), cancellationToken);

        return Unit.Value;
    }
}
