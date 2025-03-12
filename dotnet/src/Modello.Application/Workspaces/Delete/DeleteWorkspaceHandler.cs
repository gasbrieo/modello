using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces.Events;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.Workspaces.Delete;

internal sealed class DeleteWorkspaceHandler(IWorkspaceRepository repository, IUnitOfWork unitOfWork, IMediator mediator) : ICommandHandler<DeleteWorkspaceCommand, Result>
{
    public async Task<Result> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workspace is not null)
        {
            await repository.DeleteAsync(workspace, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await mediator.Publish(new WorkspaceDeletedEvent(workspace.Id), cancellationToken);
        }

        return Result.Success();
    }
}
