using Modello.Application.Workspaces.Get;

namespace Modello.Application.UnitTests.Workspaces.Get;

public class GetWorkspaceValidatorTests
{
    private readonly GetWorkspaceValidator _validator = new();

    [Fact]
    public void GivenQuery_WhenValidateCalled_ThenHasNoErrors()
    {
        // Given
        var query = new GetWorkspaceQuery(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(query);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GivenQueryWithEmptyId_WhenValidateCalled_ThenHasErrorForId()
    {
        // Given
        var query = new GetWorkspaceQuery(Guid.Empty);

        // When
        var result = _validator.TestValidate(query);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
