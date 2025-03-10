namespace Modello.Application.Workspaces.Delete;

public sealed class DeleteWorkspaceValidator : AbstractValidator<DeleteWorkspaceCommand>
{
    public DeleteWorkspaceValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .WithErrorCode("Id must not be empty.")
            .WithMessage("The identifier of the workspace cannot be empty.");
    }
}