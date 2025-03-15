using Modello.Application.Workspaces.Create;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Application.UnitTests.Workspaces.Create;

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
    public async Task GivenCommand_WhenHandleCalled_ThenCreatesWorkspaceAndReturnsWorkspace()
    {
        // Given
        var command = new CreateWorkspaceCommand("Work");
        var cancellationToken = CancellationToken.None;

        // When
        var result = await _handler.Handle(command, cancellationToken);

        // Then
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.NotEqual(string.Empty, result.Location);
        Assert.NotNull(result.Value);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal(command.Name, result.Value.Name);

        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Workspace>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
    }
}
