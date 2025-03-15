using Modello.Domain.Workspaces;

namespace Modello.Domain.UnitTests.Workspaces;

public class WorkspaceTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var name = "Work";

        // When
        var workspace = new Workspace(name);

        // Then
        Assert.Equal(name, workspace.Name);
        Assert.NotEqual(Guid.Empty, workspace.Id);
    }

    [Fact]
    public void GivenWorkspace_WhenUpdateName_ThenSetsName()
    {
        // Given
        var workspace = new Workspace("Work");
        var newName = "Study";

        // When
        workspace.UpdateName(newName);

        // Then
        Assert.Equal(newName, workspace.Name);
    }
}
