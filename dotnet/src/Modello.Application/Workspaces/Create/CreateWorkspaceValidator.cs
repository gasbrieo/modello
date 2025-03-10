namespace Modello.Application.Workspaces.Create;

public sealed class CreateWorkspaceValidator : AbstractValidator<CreateWorkspaceCommand>
{
    public CreateWorkspaceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Name must not be empty.")
            .WithMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }
}