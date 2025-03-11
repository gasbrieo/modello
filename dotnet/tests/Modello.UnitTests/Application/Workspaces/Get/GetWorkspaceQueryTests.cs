using Modello.Application.Workspaces.Get;

namespace Modello.UnitTests.Application.Workspaces.Get;

public class GetWorkspaceQueryTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var query = new GetWorkspaceQuery(id);

        // Assert
        Assert.Equal(id, query.Id);
    }
}
