using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Mapster;
using Mapster.Utils;

namespace BeerBuzz.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.ScanInheritedTypes(typeof(DependencyInjection).Assembly);

        return services;
    }
}
