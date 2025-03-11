using Modello.Application.Workspaces.Get;

namespace Modello.UnitTests.Application.Workspaces.Get;

public class GetWorkspaceValidatorTests
{
    private readonly GetWorkspaceValidator _validator = new();

    [Fact]
    public void Validate_ShouldNotHaveError()
    {
        // Arrange
        var query = new GetWorkspaceQuery(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ShouldHaveError()
    {
        // Arrange
        var query = new GetWorkspaceQuery(Guid.Empty);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
