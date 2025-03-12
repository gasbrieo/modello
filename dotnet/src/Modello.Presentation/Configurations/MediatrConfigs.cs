using Modello.Application.Common.Behaviors;
using Modello.Domain;

namespace Modello.Presentation.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Application.AssemblyReference).Assembly,
            typeof(IDomainMarker).Assembly
        };

        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies!);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}
