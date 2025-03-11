using Modello.Application.Workspaces.Create;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.UnitTests.Application.Workspaces.Create;

public class CreateWorkspaceHandlerTests
{
    private readonly Mock<IWorkspaceRepository> _repositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly CreateWorkspaceHandler _handler;

    public CreateWorkspaceHandlerTests()
    {
        _handler = new(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateWorkspace()
    {
        // Arrange
        var command = new CreateWorkspaceCommand("Work");
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(command.Name, result.Name);

        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Workspace>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
    }
}
