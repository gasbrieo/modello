using Modello.Application.Workspaces.Get;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Exceptions;
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
        Assert.Equal(workspace.Id, result.Id);
        Assert.Equal(workspace.Name, result.Name);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(query.Id, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenWorkspaceDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var query = new GetWorkspaceQuery(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(query.Id, cancellationToken)).ReturnsAsync((Workspace?)null);

        // Act & Assert
        await Assert.ThrowsAsync<WorkspaceNotFoundException>(() => _handler.Handle(query, cancellationToken));

        _repositoryMock.Verify(repo => repo.GetByIdAsync(query.Id, cancellationToken), Times.Once);
    }
}
