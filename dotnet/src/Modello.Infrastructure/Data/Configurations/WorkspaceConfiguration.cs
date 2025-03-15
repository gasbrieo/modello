using Modello.Domain.Workspaces;

namespace Modello.Infrastructure.Data.Configurations;

internal sealed class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.ToTable(nameof(Workspace));

        builder.HasKey(workspace => workspace.Id);

        builder.Property(workspace => workspace.Name)
            .IsRequired();
    }
}