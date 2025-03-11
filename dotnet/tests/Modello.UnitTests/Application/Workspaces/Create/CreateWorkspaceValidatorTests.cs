using Modello.Application.Workspaces.Create;

namespace Modello.UnitTests.Application.Workspaces.Create;

public class CreateWorkspaceValidatorTests
{
    private readonly CreateWorkspaceValidator _validator = new();

    [Fact]
    public void Validate_ShouldNotHaveError()
    {
        // Arrange
        var command = new CreateWorkspaceCommand("Work");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new CreateWorkspaceCommand(string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("Name must not be empty.")
            .WithErrorMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }
}
