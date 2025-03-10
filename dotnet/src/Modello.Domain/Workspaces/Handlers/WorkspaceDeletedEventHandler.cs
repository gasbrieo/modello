using Modello.Domain.Workspaces.Events;

namespace Modello.Domain.Workspaces.Handlers;

internal sealed class WorkspaceDeletedEventHandler(ILogger<WorkspaceDeletedEventHandler> logger) : INotificationHandler<WorkspaceDeletedEvent>
{
    public Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Workspace Deleted event for {workspaceId}", notification.WorkspaceId);
        return Task.CompletedTask;
    }
}
