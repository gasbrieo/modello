using Modello.Application.Workspaces.List;

namespace Modello.Application.UnitTests.Workspaces.List;

public class ListWorkspacesQueryTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var pageNumber = 1;
        var pageSize = 10;

        // When
        var query = new ListWorkspacesQuery(pageNumber, pageSize);

        // Then
        Assert.Equal(pageNumber, query.PageNumber);
        Assert.Equal(pageSize, query.PageSize);
    }
}