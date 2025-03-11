using Modello.Domain.Workspaces;

namespace Modello.UnitTests.Domain.Workspaces;

public class WorkspaceTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var name = "Work";

        // Act
        var workspace = new Workspace(name);

        // Assert
        Assert.Equal(name, workspace.Name);
        Assert.NotEqual(Guid.Empty, workspace.Id);
    }
}
