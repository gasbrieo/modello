using Modello.Application.Workspaces.List;

namespace Modello.Application.UnitTests.Workspaces.List;

public class ListWorkspacesValidatorTests
{
    private readonly ListWorkspacesValidator _validator = new();

    [Fact]
    public void GivenQuery_WhenValidateCalled_ThenHasNoErrors()
    {
        // Given
        var query = new ListWorkspacesQuery(1, 1);

        // When
        var result = _validator.TestValidate(query);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void GivenQueryWithPageNumberBelowMinimum_WhenValidateCalled_ThenHasErrorForPageNumber()
    {
        // Given
        var query = new ListWorkspacesQuery(0, 1);

        // When
        var result = _validator.TestValidate(query);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.PageNumber)
            .WithErrorCode("PageNumber must be greater than zero.")
            .WithErrorMessage("The page number must be greater than zero.");
    }

    [Fact]
    public void GivenQueryWithPageSizeBelowMinimum_WhenValidateCalled_ThenHasErrorForPageSize()
    {
        // Given
        var query = new ListWorkspacesQuery(1, 0);

        // When
        var result = _validator.TestValidate(query);

        // Then
        result.ShouldHaveValidationErrorFor(x => x.PageSize)
            .WithErrorCode("PageSize must be greater than zero.")
            .WithErrorMessage("The page size must be greater than zero.");
    }
}