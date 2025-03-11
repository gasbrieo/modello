using FluentValidation;
using Modello.Application.Common.Behaviors;

namespace Modello.UnitTests.Application.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly Mock<RequestHandlerDelegate<int>> _delegateMock = new();

    public ValidationBehaviorTests()
    {
        _delegateMock.Setup(n => n()).ReturnsAsync(1);
    }

    [Fact]
    public async Task Handle_WhenHasNoValidators_ShouldProcessRequest()
    {
        // Arrange
        var request = new ConcreteRequest(string.Empty);
        var validators = new List<IValidator<ConcreteRequest>>();
        var behavior = new ValidationBehavior<ConcreteRequest, int>(validators);

        // Act
        var response = await behavior.Handle(request, _delegateMock.Object, CancellationToken.None);

        // Assert
        Assert.Equal(1, response);

        _delegateMock.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenPassValidations_ShouldProcessRequest()
    {
        // Arrange
        var request = new ConcreteRequest("Name");
        var validators = new List<IValidator<ConcreteRequest>> { new ConcreteRequestValidator() };
        var behavior = new ValidationBehavior<ConcreteRequest, int>(validators);

        // Act
        var response = await behavior.Handle(request, _delegateMock.Object, CancellationToken.None);

        // Assert
        Assert.Equal(1, response);

        _delegateMock.Verify(n => n(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenFailValidations_ShouldThrowValidationException()
    {
        // Arrange
        var request = new ConcreteRequest(string.Empty);
        var validators = new List<IValidator<ConcreteRequest>> { new ConcreteRequestValidator() };
        var behavior = new ValidationBehavior<ConcreteRequest, int>(validators);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => behavior.Handle(request, _delegateMock.Object, CancellationToken.None));

        _delegateMock.Verify(n => n(), Times.Never);
    }

    public record ConcreteRequest(string Name) : IRequest<int>;

    public class ConcreteRequestValidator : AbstractValidator<ConcreteRequest>
    {
        public ConcreteRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
