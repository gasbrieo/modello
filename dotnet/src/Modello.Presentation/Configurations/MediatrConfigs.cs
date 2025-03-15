using Modello.Application;
using Modello.Domain;

namespace Modello.Presentation.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(IApplicationMarker).Assembly,
            typeof(IDomainMarker).Assembly
        };

        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies!);
                cfg.AddOpenBehavior(typeof(ValidationAsExceptionBehavior<,>));
            })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}
