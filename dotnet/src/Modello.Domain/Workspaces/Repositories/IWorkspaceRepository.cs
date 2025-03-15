namespace Modello.Domain.Workspaces.Repositories;

public interface IWorkspaceRepository
{
    Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default);

    Task UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default);

    Task DeleteAsync(Workspace workspace, CancellationToken cancellationToken = default);
}
