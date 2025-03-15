using Modello.Application.Workspaces.Delete;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Events;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.UnitTests.Workspaces.Delete;

public class DeleteWorkspaceHandlerTests
{
    private readonly Mock<IWorkspaceRepository> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly DeleteWorkspaceHandler _handler;

    public DeleteWorkspaceHandlerTests()
    {
        _handler = new(_repositoryMock.Object, _unitOfWorkMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task GivenCommand_WhenHandleCalled_ThenDeletesWorkspaceAndReturnsNoContent()
    {
        // Given
        var command = new DeleteWorkspaceCommand(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        var workspace = new Workspace("Work");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync(workspace);

        // When
        var result = await _handler.Handle(command, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.NoContent, result.Status);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Workspace>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
        _mediatorMock.Verify(med => med.Publish(It.IsAny<WorkspaceDeletedEvent>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenCommandWithNonExistingWorkspace_WhenHandleCalled_ThenReturnsNoContent()
    {
        // Given
        var command = new DeleteWorkspaceCommand(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync((Workspace?)null);

        // When
        var result = await _handler.Handle(command, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.NoContent, result.Status);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
        _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Workspace>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _mediatorMock.Verify(med => med.Publish(It.IsAny<WorkspaceDeletedEvent>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
