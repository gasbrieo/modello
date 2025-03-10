using Modello.Domain.Common.Interfaces;
using Modello.Domain.Workspaces.Exceptions;

namespace Modello.Domain.Workspaces;

public sealed class Workspace : IAggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    public Workspace(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new WorkspaceNameEmptyException();
        }

        Name = name;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new WorkspaceNameEmptyException();
        }

        Name = name;
    }
}
