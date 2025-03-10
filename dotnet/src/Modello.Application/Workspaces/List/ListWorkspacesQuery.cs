using Modello.Application.Common.Messaging;

namespace Modello.Application.Workspaces.List;

public record ListWorkspacesQuery : IQuery<IEnumerable<WorkspaceDto>>;
