using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.Get;

public sealed record GetWorkspaceQuery(Guid Id) : IQuery<WorkspaceDto>;
