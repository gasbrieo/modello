namespace Modello.Domain.Workspaces.Events;

public record WorkspaceDeletedEvent(Guid Id) : INotification;

