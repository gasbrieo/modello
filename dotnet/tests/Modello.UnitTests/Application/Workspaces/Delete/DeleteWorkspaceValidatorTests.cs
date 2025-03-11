using Modello.Application.Workspaces.Delete;

namespace Modello.UnitTests.Application.Workspaces.Delete;

public class DeleteWorkspaceValidatorTests
{
    private readonly DeleteWorkspaceValidator _validator = new();

    [Fact]
    public void Validate_ShouldNotHaveError()
    {
        // Arrange
        var command = new DeleteWorkspaceCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new DeleteWorkspaceCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
