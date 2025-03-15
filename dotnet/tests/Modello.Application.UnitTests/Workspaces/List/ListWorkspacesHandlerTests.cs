using Modello.Application.Workspaces;
using Modello.Application.Workspaces.List;

namespace Modello.Application.UnitTests.Workspaces.List;

public class ListWorkspacesHandlerTests
{
    private readonly Mock<IListWorkspacesService> _serviceMock = new();
    private readonly ListWorkspacesHandler _handler;

    public ListWorkspacesHandlerTests()
    {
        _handler = new(_serviceMock.Object);
    }

    [Fact]
    public async Task GivenQuery_WhenHandleCalled_ThenReturnsPagedWorkspaces()
    {
        // Given
        var query = new ListWorkspacesQuery(1, 1);
        var cancellationToken = CancellationToken.None;

        var pagedList = new PagedList<WorkspaceDto>(1, 10, 100, []);
        _serviceMock.Setup(serv => serv.ListAsync(query, cancellationToken)).ReturnsAsync(pagedList);

        // When
        var result = await _handler.Handle(query, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(pagedList, result.Value);

        _serviceMock.Verify(serv => serv.ListAsync(query, cancellationToken), Times.Once);
    }
}
