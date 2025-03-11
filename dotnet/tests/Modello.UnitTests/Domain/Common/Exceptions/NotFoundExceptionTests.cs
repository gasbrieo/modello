using Modello.Domain.Common.Exceptions;

namespace Modello.UnitTests.Domain.Common.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var error = "Workspace not found.";
        var detail = "The workspace with the provided identifier was not found.";

        // Act
        var exception = new ConcreteNotFoundException(error, detail);

        // Assert
        Assert.Equal(error, exception.Error);
        Assert.Equal(detail, exception.Detail);
    }

    public class ConcreteNotFoundException(string error, string detail) : NotFoundException(error, detail);
}
