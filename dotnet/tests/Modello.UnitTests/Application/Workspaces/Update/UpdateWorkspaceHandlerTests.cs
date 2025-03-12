﻿using Modello.Application.Common.Results;
using Modello.Application.Workspaces.Update;
using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.UnitTests.Application.Workspaces.Update;

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
    public async Task Handle_ShouldReturnWorkspace()
    {
        // Arrange
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");
        var cancellationToken = CancellationToken.None;

        var workspace = new Workspace("Study");
        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync(workspace);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(workspace.Id, result.Value.Id);
        Assert.Equal(workspace.Name, result.Value.Name);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Workspace>(), cancellationToken), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenWorkspaceDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new UpdateWorkspaceCommand(Guid.NewGuid(), "Work");
        var cancellationToken = CancellationToken.None;

        _repositoryMock.Setup(repo => repo.GetByIdAsync(command.Id, cancellationToken)).ReturnsAsync((Workspace?)null);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);

        _repositoryMock.Verify(repo => repo.GetByIdAsync(command.Id, cancellationToken), Times.Once);
    }
}