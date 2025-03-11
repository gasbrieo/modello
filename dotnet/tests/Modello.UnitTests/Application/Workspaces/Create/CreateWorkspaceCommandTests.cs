using Modello.Application.Workspaces.Create;

namespace Modello.UnitTests.Application.Workspaces.Create;

public class CreateWorkspaceCommandTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var name = "Work";

        // Act
        var command = new CreateWorkspaceCommand(name);

        // Assert
        Assert.Equal(name, command.Name);
    }
}
