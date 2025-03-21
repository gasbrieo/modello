﻿using Modello.Application.Workspaces.Create;
using Modello.Application.Workspaces.Delete;
using Modello.Application.Workspaces.Get;
using Modello.Application.Workspaces.List;
using Modello.Application.Workspaces.Update;
using Modello.Presentation.Requests.V1;

namespace Modello.Presentation.Controllers.V1;

public sealed class WorkspacesController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> ListWorkspaces([FromQuery] ListWorkspacesRequest request, CancellationToken cancellationToken)
    {
        var query = new ListWorkspacesQuery(request.PageNumber, request.PageSize);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpGet("{workspaceId:guid}")]
    public async Task<IActionResult> GetWorkspace(Guid workspaceId, CancellationToken cancellationToken)
    {
        var query = new GetWorkspaceQuery(workspaceId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkspace([FromBody] CreateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWorkspaceCommand(request.Name);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPut("{workspaceId:guid}")]
    public async Task<IActionResult> UpdateWorkspace(Guid workspaceId, [FromBody] UpdateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWorkspaceCommand(workspaceId, request.Name);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpDelete("{workspaceId:guid}")]
    public async Task<IActionResult> DeleteWorkspace(Guid workspaceId, CancellationToken cancellationToken)
    {
        var command = new DeleteWorkspaceCommand(workspaceId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }
}