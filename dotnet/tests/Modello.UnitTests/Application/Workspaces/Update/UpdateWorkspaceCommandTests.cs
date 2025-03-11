using Modello.Application.Workspaces.Update;

namespace Modello.UnitTests.Application.Workspaces.Update;

public class UpdateWorkspaceCommandTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Work";

        // Act
        var command = new UpdateWorkspaceCommand(id, name);

        // Assert
        Assert.Equal(id, command.Id);
        Assert.Equal(name, command.Name);
    }
}
