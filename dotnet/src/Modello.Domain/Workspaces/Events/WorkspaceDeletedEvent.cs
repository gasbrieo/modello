namespace Modello.Domain.Workspaces.Events;

public sealed record WorkspaceDeletedEvent(Guid Id) : INotification;

