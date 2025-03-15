using Modello.Application.Workspaces.Delete;

namespace Modello.Application.UnitTests.Workspaces.Delete;

public class DeleteWorkspaceCommandTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        var command = new DeleteWorkspaceCommand(id);

        // Then
        Assert.Equal(id, command.Id);
    }
}
