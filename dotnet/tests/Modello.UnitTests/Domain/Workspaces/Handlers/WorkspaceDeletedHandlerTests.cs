using Modello.Domain.Workspaces.Events;
using Modello.Domain.Workspaces.Handlers;
using Modello.UnitTests.TestHelpers;

namespace Modello.UnitTests.Domain.Workspaces.Handlers;

public class WorkspaceDeletedHandlerTests
{
    private readonly Mock<ILogger<WorkspaceDeletedHandler>> _loggerMock = new();
    private readonly WorkspaceDeletedHandler _handler;

    public WorkspaceDeletedHandlerTests()
    {
        _handler = new(_loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldLogInformation()
    {
        // Arrange
        var notification = new WorkspaceDeletedEvent(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(notification, cancellationToken);

        // Assert
        _loggerMock.VerifyLog(LogLevel.Information, $"Handling Workspace Deleted event for '{notification.Id}'", Times.Once);
    }
}
