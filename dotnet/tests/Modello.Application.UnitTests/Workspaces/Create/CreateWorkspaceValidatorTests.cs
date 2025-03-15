using Modello.Application.Workspaces.Create;

namespace Modello.Application.UnitTests.Workspaces.Create;

public class CreateWorkspaceValidatorTests
{
    private readonly CreateWorkspaceValidator _validator = new();

    [Fact]
    public void GivenCommand_WhenValidateCalled_ThenHasNoErrors()
    {
        // Given
        var command = new CreateWorkspaceCommand("Work");

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GivenCommandWithEmptyName_WhenValidateCalled_ThenHasErrorForName()
    {
        // Given
        var command = new CreateWorkspaceCommand(string.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode("Name must not be empty.")
            .WithErrorMessage("The name of the workspace cannot be empty or contain only white spaces.");
    }
}
