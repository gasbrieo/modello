using Modello.Application.Workspaces.Get;

namespace Modello.Application.UnitTests.Workspaces.Get;

public class GetWorkspaceQueryTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        var query = new GetWorkspaceQuery(id);

        // Then
        Assert.Equal(id, query.Id);
    }
}
