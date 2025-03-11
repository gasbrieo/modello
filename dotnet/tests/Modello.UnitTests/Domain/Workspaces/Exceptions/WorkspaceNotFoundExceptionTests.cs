using Modello.Domain.Workspaces.Exceptions;

namespace Modello.UnitTests.Domain.Workspaces.Exceptions;

public class WorkspaceNotFoundExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Act
        var exception = new WorkspaceNotFoundException();

        // Assert
        Assert.Equal("Workspace not found.", exception.Error);
        Assert.Equal("The workspace with the provided identifier was not found.", exception.Detail);
    }
}
