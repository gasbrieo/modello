using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;

namespace Modello.Application.Workspaces.Create;

public sealed record CreateWorkspaceCommand(string Name) : ICommand<Result<WorkspaceDto>>;
