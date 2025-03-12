using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;

namespace Modello.Application.Workspaces.Get;

public sealed record GetWorkspaceQuery(Guid Id) : IQuery<Result<WorkspaceDto>>;
