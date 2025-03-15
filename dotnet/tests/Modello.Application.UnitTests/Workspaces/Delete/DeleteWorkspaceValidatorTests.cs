using Modello.Application.Workspaces.Delete;

namespace Modello.Application.UnitTests.Workspaces.Delete;

public class DeleteWorkspaceValidatorTests
{
    private readonly DeleteWorkspaceValidator _validator = new();

    [Fact]
    public void GivenCommand_WhenValidateCalled_ThenHasNoErrors()
    {
        // Given
        var command = new DeleteWorkspaceCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GivenCommandWithEmptyId_WhenValidateCalled_ThenHasErrorForId()
    {
        // Given
        var command = new DeleteWorkspaceCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode("Id must not be empty.")
            .WithErrorMessage("The identifier of the workspace cannot be empty.");
    }
}
