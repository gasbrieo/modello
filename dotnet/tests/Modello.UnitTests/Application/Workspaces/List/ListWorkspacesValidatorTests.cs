using Modello.Application.Workspaces.List;

namespace Modello.UnitTests.Application.Workspaces.List;

public class ListWorkspacesValidatorTests
{
    private readonly ListWorkspacesValidator _validator = new();

    [Fact]
    public void Validate_ShouldNotHaveError()
    {
        // Arrange
        var query = new ListWorkspacesQuery(1, 1);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenPageNumberIsLessThanOne_ShouldHaveError()
    {
        // Arrange
        var query = new ListWorkspacesQuery(0, 1);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageNumber)
            .WithErrorCode("PageNumber must be greater than zero.")
            .WithErrorMessage("The page number must be greater than zero.");
    }

    [Fact]
    public void Validate_WhenPageSizeIsLessThanOne_ShouldHaveError()
    {
        // Arrange
        var query = new ListWorkspacesQuery(1, 0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize)
            .WithErrorCode("PageSize must be greater than zero.")
            .WithErrorMessage("The page size must be greater than zero.");
    }
}