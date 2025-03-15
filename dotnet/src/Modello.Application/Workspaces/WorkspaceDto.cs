namespace Modello.Application.Workspaces;

public record WorkspaceDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public WorkspaceDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}