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

    [Fact]
    public void UpdateName_ShouldSetName()
    {
        // Arrange
        var workspace = new Workspace("Work");
        var newName = "Study";

        // Act
        workspace.UpdateName(newName);

        // Assert
        Assert.Equal(newName, workspace.Name);
    }
}
