namespace Modello.Presentation.Configurations;

public static class FluentValidationConfigs
{
    public static IServiceCollection AddFluentValidationConfigs(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(Application.AssemblyReference).Assembly,
        };

        return services.AddValidatorsFromAssemblies(assemblies);
    }
}
