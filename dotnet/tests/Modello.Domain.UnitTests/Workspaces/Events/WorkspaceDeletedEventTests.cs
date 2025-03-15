using Modello.Domain.Workspaces.Events;

namespace Modello.Domain.UnitTests.Workspaces.Events;

public class WorkspaceDeletedEventTests
{
    [Fact]
    public void GivenValidParameters_WhenInitialize_ThenPropertiesAreSet()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        var notification = new WorkspaceDeletedEvent(id);

        // Then
        Assert.Equal(id, notification.Id);
    }
}
