namespace Modello.Application.Workspaces.Update;

public sealed class UpdateWorkspaceValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .WithErrorCode("Id must not be empty.")
            .WithMessage("The identifier of the workspace cannot be empty.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Name must not be empty.")
            .WithMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }
}
