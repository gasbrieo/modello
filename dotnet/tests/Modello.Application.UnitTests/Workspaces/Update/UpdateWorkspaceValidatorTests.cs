using Modello.Application.Workspaces.Update;

namespace Modello.Application.UnitTests.Workspaces.Update;

public class UpdateWorkspaceValidatorTests
{
    private readonly UpdateWorkspaceValidator _validator = new();

    [Fact]
    public void GivenCommand_WhenValidateCalled_ThenHasNoErrors()
    {
        // Given
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GivenCommandWithEmptyName_WhenValidateCalled_ThenHasErrorForName()
    {
        // Given
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), string.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("Name must not be empty.")
            .WithErrorMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }

    [Fact]
    public void GivenCommandWithEmptyId_WhenValidateCalled_ThenHasErrorForId()
    {
        // Given
        var command = new UpdateWorkspaceCommand(Guid.Empty, "Work");

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
