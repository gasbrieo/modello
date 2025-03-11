using Modello.Application.Common.Pagination;

namespace Modello.UnitTests.Application.Common.Pagination;

public class PagedListTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesProperly()
    {
        // Arrange
        var items = new List<ConcreteDto>();
        var count = 20;
        var pageNumber = 1;
        var pageSize = 10;

        // Act
        var pagedList = new PagedList<ConcreteDto>(items, count, pageNumber, pageSize);

        // Assert
        Assert.Equal(items, pagedList.Items);
        Assert.Equal(pageNumber, pagedList.PageNumber);
        Assert.Equal(2, pagedList.TotalPages);
        Assert.Equal(count, pagedList.TotalCount);
        Assert.False(pagedList.HasPreviousPage);
        Assert.True(pagedList.HasNextPage);
    }

    [Fact]
    public void HasPreviousPage_WhenPageNumberIsGreaterThanOne_ShouldBeTrue()
    {
        // Arrange
        var pagedList = new PagedList<ConcreteDto>([], 20, 2, 10);

        // Act & Assert
        Assert.True(pagedList.HasPreviousPage);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public void HasPreviousPage_WhenPageNumberIsLessOrEqualToOne_ShouldBeFalse(int pageNumber)
    {
        // Arrange
        var pagedList = new PagedList<ConcreteDto>([], 20, pageNumber, 10);

        // Act & Assert
        Assert.False(pagedList.HasPreviousPage);
    }

    [Fact]
    public void HasNextPage_WhenPageNumberIsLessThanTotalPages_ShouldBeTrue()
    {
        // Arrange
        var pagedList = new PagedList<ConcreteDto>([], 20, 1, 10);

        // Act & Assert
        Assert.True(pagedList.HasNextPage);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void HasNextPage_WhenPageNumberIsGreaterOrEqualToTotalPages_ShouldBeFalse(int pageNumber)
    {
        // Arrange
        var pagedList = new PagedList<ConcreteDto>([], 20, pageNumber, 10);

        // Act & Assert
        Assert.False(pagedList.HasNextPage);
    }

    public record ConcreteDto;
}
