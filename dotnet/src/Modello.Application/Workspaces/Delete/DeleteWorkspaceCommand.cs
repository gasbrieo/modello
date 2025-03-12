using Modello.Application.Common.Messaging;
using Modello.Application.Common.Results;

namespace Modello.Application.Workspaces.Delete;

public sealed record DeleteWorkspaceCommand(Guid Id) : ICommand<Result>;
