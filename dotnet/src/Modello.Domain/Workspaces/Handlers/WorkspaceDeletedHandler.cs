using Modello.Domain.Workspaces.Events;

namespace Modello.Domain.Workspaces.Handlers;

internal sealed class WorkspaceDeletedHandler(ILogger<WorkspaceDeletedHandler> logger) : INotificationHandler<WorkspaceDeletedEvent>
{
    public Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Workspace Deleted event for '{id}'", notification.Id);
        return Task.CompletedTask;
    }
}

