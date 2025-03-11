using Modello.Domain.Common.Exceptions;

namespace Modello.UnitTests.Domain.Common.Exceptions;

public class BadRequestExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var error = "Name must not be empty.";
        var detail = "The name of the workspace cannot be empty or contain only white spaces.";

        // Act
        var exception = new ConcreteBadRequestException(error, detail);

        // Assert
        Assert.Equal(error, exception.Error);
        Assert.Equal(detail, exception.Detail);
    }

    public class ConcreteBadRequestException(string error, string detail) : BadRequestException(error, detail);
}