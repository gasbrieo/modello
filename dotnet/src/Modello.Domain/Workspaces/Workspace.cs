namespace Modello.Domain.Workspaces;

public sealed class Workspace(string name) : IAggregateRoot
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;

    public void UpdateName(string name)
    {
        Name = name;
    }
}
