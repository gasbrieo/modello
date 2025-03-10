namespace Modello.Domain.Workspaces.Events;

public record WorkspaceDeletedEvent(Guid WorkspaceId) : INotification;

