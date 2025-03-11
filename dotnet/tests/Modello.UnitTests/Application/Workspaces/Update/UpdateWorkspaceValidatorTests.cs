using Modello.Application.Workspaces.Update;

namespace Modello.UnitTests.Application.Workspaces.Update;

public class UpdateWorkspaceValidatorTests
{
    private readonly UpdateWorkspaceValidator _validator = new();

    [Fact]
    public void Validate_ShouldNotHaveError()
    {
        // Arrange
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("Name must not be empty.")
            .WithErrorMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new UpdateWorkspaceCommand(Guid.Empty, "Work");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
