namespace Modello.Application.Workspaces.Get;

public sealed class GetWorkspaceQueryValidator : AbstractValidator<GetWorkspaceQuery>
{
    public GetWorkspaceQueryValidator()
    {
        RuleFor(e => e.WorkspaceId).NotEmpty();
    }
}