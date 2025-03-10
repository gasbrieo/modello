namespace Modello.Application.Workspaces.Update;

public sealed class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidator()
    {
        RuleFor(e => e.WorkspaceId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
