using Modello.Application.Workspaces.Update;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.UnitTests.Workspaces.Update;

public class UpdateWorkspaceHandlerTests
{
    private readonly Mock<IWorkspaceRepository> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly UpdateWorkspaceHandler _handler;

    public UpdateWorkspaceHandlerTests()
    {
        _handler = new(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GivenCommand_WhenHandleCalled_ThenUpdatesWorkspaceAndReturnsWorkspace()
    {
        // Given
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");
        var cancellationToken = CancellationToken.None;

        var workspace = new Workspace("Study");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync(workspace);

        // When
        var result = await _handler.Handle(command, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(workspace.Id, result.Value.Id);
        Assert.Equal(workspace.Name, result.Value.Name);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Workspace>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenCommandWithNonExistingWorkspace_WhenHandleCalled_ThenReturnsNotFound()
    {
        // Given
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync((Workspace?)null);

        // When
        var result = await _handler.Handle(command, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.NotFound, result.Status);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
    }
}