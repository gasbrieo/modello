namespace Modello.Application.Workspaces.Get;

public sealed class GetWorkspaceValidator : AbstractValidator<GetWorkspaceQuery>
{
    public GetWorkspaceValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .WithErrorCode("Id must not be empty.")
            .WithMessage("The identifier of the workspace cannot be empty.");
    }
}