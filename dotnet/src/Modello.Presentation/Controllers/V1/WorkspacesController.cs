using Modello.Application.Workspaces.Create;
using Modello.Application.Workspaces.Delete;
using Modello.Application.Workspaces.Get;
using Modello.Application.Workspaces.List;
using Modello.Application.Workspaces.Update;
using Modello.Presentation.Requests.V1;
using Modello.Presentation.Responses;

namespace Modello.Presentation.Controllers.V1;

public sealed class WorkspacesController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> ListWorkspaces([FromQuery] ListWorkspacesRequest request, CancellationToken cancellationToken)
    {
        var query = new ListWorkspacesQuery(request.PageNumber, request.PageSize);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{workspaceId:guid}")]
    public async Task<IActionResult> GetWorkspace(Guid workspaceId, CancellationToken cancellationToken)
    {
        var query = new GetWorkspaceQuery(workspaceId);
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        var error = ErrorResponse.FromContext(HttpContext);
        error.SetErrors(result.Errors);

        return NotFound(error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWorkspaceCommand(request.Name);
        var result = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetWorkspace), new { WorkspaceId = result.Value!.Id }, result.Value);
    }

    [HttpPut("{workspaceId:guid}")]
    public async Task<IActionResult> UpdateWorkspace(Guid workspaceId, [FromBody] UpdateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWorkspaceCommand(workspaceId, request.Name);
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        var error = ErrorResponse.FromContext(HttpContext);
        error.SetErrors(result.Errors);

        return NotFound(error);
    }

    [HttpDelete("{workspaceId:guid}")]
    public async Task<IActionResult> DeleteWorkspace(Guid workspaceId, CancellationToken cancellationToken)
    {
        var command = new DeleteWorkspaceCommand(workspaceId);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}