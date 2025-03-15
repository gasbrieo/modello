using Modello.Application.Workspaces.Create;

namespace Modello.Application.UnitTests.Workspaces.Create;

public class CreateWorkspaceCommandTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var name = "Work";

        // When
        var command = new CreateWorkspaceCommand(name);

        // Then
        Assert.Equal(name, command.Name);
    }
}
