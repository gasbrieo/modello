using Modello.Application.Workspaces.Delete;

namespace Modello.UnitTests.Application.Workspaces.Delete;

public class DeleteWorkspaceCommandTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var command = new DeleteWorkspaceCommand(id);

        // Assert
        Assert.Equal(id, command.Id);
    }
}
