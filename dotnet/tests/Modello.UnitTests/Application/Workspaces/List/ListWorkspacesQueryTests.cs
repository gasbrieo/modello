using Modello.Application.Workspaces.List;

namespace Modello.UnitTests.Application.Workspaces.List;

public class ListWorkspacesQueryTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        // Act
        var query = new ListWorkspacesQuery(pageNumber, pageSize);

        // Assert
        Assert.Equal(pageNumber, query.PageNumber);
        Assert.Equal(pageSize, query.PageSize);
    }
}