using Modello.Application.Workspaces.List;
using Modello.Domain.Workspaces.Repositories;
using Modello.Infrastructure.Data;
using Modello.Infrastructure.Data.Repositories;
using Modello.Infrastructure.Data.Services;

namespace Modello.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("Modello"));

        services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<AppDbContext>());

        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IListWorkspacesService, ListWorkspacesService>();

        return services;
    }
}
