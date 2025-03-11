using Modello.Domain.Workspaces.Events;

namespace Modello.UnitTests.Domain.Workspaces.Events;

public class WorkspaceDeletedEventTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var notification = new WorkspaceDeletedEvent(id);

        // Assert
        Assert.Equal(id, notification.Id);
    }
}
