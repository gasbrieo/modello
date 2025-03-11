using Modello.Domain.Workspaces.Exceptions;

namespace Modello.UnitTests.Domain.Workspaces.Exceptions;

public class WorkspaceNameEmptyExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Act
        var exception = new WorkspaceNameEmptyException();

        // Assert
        Assert.Equal("Name must not be empty.", exception.Error);
        Assert.Equal("The name of the workspace cannot be empty or contain only white spaces.", exception.Detail);
    }
}
