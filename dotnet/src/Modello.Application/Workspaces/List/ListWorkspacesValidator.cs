namespace Modello.Application.Workspaces.List;

public sealed class ListWorkspacesValidator : AbstractValidator<ListWorkspacesQuery>
{
    public ListWorkspacesValidator()
    {
        RuleFor(e => e.PageNumber)
            .GreaterThan(0)
            .WithErrorCode("PageNumber must be greater than zero.")
            .WithMessage("The page number must be greater than zero.");

        RuleFor(e => e.PageSize)
            .GreaterThan(0)
            .WithErrorCode("PageSize must be greater than zero.")
            .WithMessage("The page size must be greater than zero.");
    }
}