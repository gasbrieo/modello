using Modello.Application.Common.Pagination;
using Modello.Application.Common.Results;
using Modello.Application.Workspaces;
using Modello.Application.Workspaces.Create;
using Modello.Application.Workspaces.Delete;
using Modello.Application.Workspaces.Get;
using Modello.Application.Workspaces.List;
using Modello.Application.Workspaces.Update;
using Modello.Presentation.Controllers.V1;
using Modello.Presentation.Requests.V1;

namespace Modello.UnitTests.Presentation.Controllers.V1;

public class WorkspacesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly WorkspacesController _controller;

    public WorkspacesControllerTests()
    {
        _controller = new(_mediatorMock.Object);
    }

    [Fact]
    public async Task ListWorkspaces_ShouldReturnOk()
    {
        // Arrange
        var request = new ListWorkspacesRequest() { PageNumber = 1, PageSize = 10 };
        var cancellationToken = CancellationToken.None;

        var pagedList = new PagedList<WorkspaceDto>([], 0, request.PageNumber, request.PageSize);
        _mediatorMock.Setup(m => m.Send(It.IsAny<ListWorkspacesQuery>(), cancellationToken))
            .ReturnsAsync(pagedList);

        // Act
        var result = await _controller.ListWorkspaces(request, cancellationToken);

        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<PagedList<WorkspaceDto>>(objectResult.Value);
        Assert.Equal(pagedList, response);

        _mediatorMock.Verify(m => m.Send(It.IsAny<ListWorkspacesQuery>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetWorkspace_ShouldReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;

        var workspaceDto = new WorkspaceDto(Guid.NewGuid(), "Work");
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWorkspaceQuery>(), cancellationToken)).ReturnsAsync(workspaceDto);

        // Act
        var result = await _controller.GetWorkspace(id, cancellationToken);

        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<WorkspaceDto>(objectResult.Value);
        Assert.Equal(workspaceDto, response);

        _mediatorMock.Verify(m => m.Send(It.IsAny<GetWorkspaceQuery>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task CreateWorkspace_ShouldReturnOk()
    {
        // Arrange
        var request = new CreateWorkspaceRequest() { Name = "Work" };
        var cancellationToken = CancellationToken.None;

        var workspaceDto = new WorkspaceDto(Guid.NewGuid(), request.Name);
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateWorkspaceCommand>(), cancellationToken)).ReturnsAsync(workspaceDto);

        // Act
        var result = await _controller.CreateWorkspace(request, cancellationToken);

        // Assert
        var objectResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(WorkspacesController.GetWorkspace), objectResult.ActionName);

        var response = Assert.IsType<WorkspaceDto>(objectResult.Value);
        Assert.Equal(workspaceDto, response);

        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateWorkspaceCommand>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpdateWorkspace_ShouldReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateWorkspaceRequest() { Name = "Work" };
        var cancellationToken = CancellationToken.None;

        var workspaceDto = new WorkspaceDto(Guid.NewGuid(), request.Name);
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateWorkspaceCommand>(), cancellationToken)).ReturnsAsync(workspaceDto);

        // Act
        var result = await _controller.UpdateWorkspace(id, request, cancellationToken);

        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<WorkspaceDto>(objectResult.Value);
        Assert.Equal(workspaceDto, response);

        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateWorkspaceCommand>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteWorkspace_ShouldReturnOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteWorkspaceCommand>(), cancellationToken)).ReturnsAsync(Result.Success());

        // Act
        var result = await _controller.DeleteWorkspace(id, cancellationToken);

        // Assert
        Assert.IsType<NoContentResult>(result);

        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteWorkspaceCommand>(), cancellationToken), Times.Once);
    }
}
