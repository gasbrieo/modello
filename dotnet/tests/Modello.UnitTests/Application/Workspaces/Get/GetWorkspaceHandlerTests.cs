using Modello.Application.Common.Results;
using Modello.Application.Workspaces.Get;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.UnitTests.Application.Workspaces.Get;

public class GetWorkspaceHandlerTests
{
    private readonly Mock<IWorkspaceRepository> _repositoryMock = new();
    private readonly GetWorkspaceHandler _handler;

    public GetWorkspaceHandlerTests()
    {
        _handler = new(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnWorkspace()
    {
        // Arrange
        var query = new GetWorkspaceQuery(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        var workspace = new Workspace("Work");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(query.Id, cancellationToken)).ReturnsAsync(workspace);

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(workspace.Id, result.Value.Id);
        Assert.Equal(workspace.Name, result.Value.Name);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(query.Id, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenWorkspaceDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var query = new GetWorkspaceQuery(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(query.Id, cancellationToken)).ReturnsAsync((Workspace?)null);

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(query.Id, cancellationToken), Times.Once);
    }
}
