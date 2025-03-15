using Modello.Domain.UnitTests.TestHelpers;
using Modello.Domain.Workspaces.Events;
using Modello.Domain.Workspaces.Handlers;

namespace Modello.Domain.UnitTests.Workspaces.Handlers;

public class WorkspaceDeletedHandlerTests
{
    private readonly Mock<ILogger<WorkspaceDeletedHandler>> _loggerMock = new();
    private readonly WorkspaceDeletedHandler _handler;

    public WorkspaceDeletedHandlerTests()
    {
        _handler = new(_loggerMock.Object);
    }

    [Fact]
    public async Task GivenEvent_WhenHandleCalled_ThenLogsInformation()
    {
        // Given
        var notification = new WorkspaceDeletedEvent(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        // When
        await _handler.Handle(notification, cancellationToken);

        // Then
        _loggerMock.VerifyLog(LogLevel.Information, $"Handling Workspace Deleted event for '{notification.Id}'", Times.Once);
    }
}
