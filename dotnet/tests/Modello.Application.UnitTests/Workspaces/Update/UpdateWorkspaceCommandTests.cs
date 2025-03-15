using Modello.Application.Workspaces.Update;

namespace Modello.Application.UnitTests.Workspaces.Update;

public class UpdateWorkspaceCommandTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var id = Guid.NewGuid();
        var name = "Work";

        // When
        var command = new UpdateWorkspaceCommand(id, name);

        // Then
        Assert.Equal(id, command.Id);
        Assert.Equal(name, command.Name);
    }
}
