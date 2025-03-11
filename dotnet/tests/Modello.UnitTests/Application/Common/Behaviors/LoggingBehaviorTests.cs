using Modello.Application.Common.Behaviors;
using Modello.UnitTests.TestHelpers;

namespace Modello.UnitTests.Application.Common.Behaviors;

public class LoggingBehaviorTests
{
    private readonly Mock<ILogger<Mediator>> _loggerMock = new();
    private readonly Mock<RequestHandlerDelegate<int>> _delegateMock = new();
    private readonly LoggingBehavior<ConcreteRequest, int> _behavior;

    public LoggingBehaviorTests()
    {
        _behavior = new LoggingBehavior<ConcreteRequest, int>(_loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldLogInformation()
    {
        // Arrange
        var request = new ConcreteRequest();
        _delegateMock.Setup(n => n()).ReturnsAsync(1);

        // Act
        var response = await _behavior.Handle(request, _delegateMock.Object, CancellationToken.None);

        // Assert
        Assert.Equal(1, response);

        _delegateMock.Verify(handler => handler(), Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, $"Handling {nameof(ConcreteRequest)}", Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, $"Handled {nameof(ConcreteRequest)}", Times.Once);
    }

    [Fact]
    public async Task Handle_WhenThrowsException_ShouldLogError()
    {
        // Arrange
        var request = new ConcreteRequest();
        _delegateMock.Setup(n => n()).ThrowsAsync(new Exception("Exception"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _behavior.Handle(request, _delegateMock.Object, CancellationToken.None));

        _delegateMock.Verify(handler => handler(), Times.Once);
        _loggerMock.VerifyLog(LogLevel.Information, $"Handling {nameof(ConcreteRequest)}", Times.Once);
        _loggerMock.VerifyLog(LogLevel.Error, $"Error while handling {nameof(ConcreteRequest)}", Times.Once);
    }

    public record ConcreteRequest : IRequest<int>;
}
