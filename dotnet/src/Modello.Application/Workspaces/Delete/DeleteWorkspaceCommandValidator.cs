namespace Modello.Application.Workspaces.Delete;

public sealed class DeleteWorkspaceCommandValidator : AbstractValidator<DeleteWorkspaceCommand>
{
    public DeleteWorkspaceCommandValidator()
    {
        RuleFor(e => e.WorkspaceId).NotEmpty();
    }
}