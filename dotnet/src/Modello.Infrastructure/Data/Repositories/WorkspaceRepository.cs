using Modello.Domain.Workspaces;
using Modello.Domain.Workspaces.Repositories;

namespace Modello.Infrastructure.Data.Repositories;

public sealed class WorkspaceRepository(AppDbContext context) : IWorkspaceRepository
{
    public Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Workspaces.FirstOrDefaultAsync(workspace => workspace.Id == id, cancellationToken);
    }

    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        await context.Workspaces.AddAsync(workspace, cancellationToken);
    }

    public Task UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        context.Workspaces.Update(workspace);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        context.Workspaces.Remove(workspace);
        return Task.CompletedTask;
    }
}
