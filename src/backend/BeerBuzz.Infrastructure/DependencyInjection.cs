using BeerBuzz.Domain.Common.Utils;
using BeerBuzz.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BeerBuzz.Domain.Abstractions.Repositories;
using BeerBuzz.Infrastructure.Repositories;
using BeerBuzz.Domain.Abstractions;
using BeerBuzz.Infrastructure.Database.Seeds;

namespace BeerBuzz.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton(x =>
        {
            var dbHost = EnvFetcher.GetRequiredEnvVariable("DATABASE_HOST");
            var dbPort = EnvFetcher.GetRequiredEnvVariable("DATABASE_PORT");
            var dbName = EnvFetcher.GetRequiredEnvVariable("DATABASE_NAME");
            var dbUsername = EnvFetcher.GetRequiredEnvVariable("DATABASE_USERNAME");
            var dbPassword = EnvFetcher.GetRequiredEnvVariable("DATABASE_PASSWORD");

            var accessKey = EnvFetcher.GetRequiredEnvVariable("MINIO_SERVER_ACCESS_KEY");
            var secretKey = EnvFetcher.GetRequiredEnvVariable("MINIO_SERVER_SECRET_KEY");
            var endpoint = EnvFetcher.GetRequiredEnvVariable("MINIO_ENDPOINT");

            return new InfrastructureConfiguration
            {
                DatabaseSettings =
                    new DbSettings(dbHost, dbPort, dbName, dbUsername, dbPassword),
                FileStoreSettings =
                    new FileStorageSettings(accessKey, secretKey, endpoint),
            };
        });

        services.AddDatabase()
            .AddRepositories()
            .AddSeeders();

    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            (serviceProvider, opt) =>
            {
                var dbSettings = serviceProvider
                    .GetRequiredService<InfrastructureConfiguration>()
                    .DatabaseSettings;

                opt.UseNpgsql(dbSettings.ConnectionString);
            });

        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBeerRepository, BeerRepository>();

        return services;
    }

    private static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        services.AddScoped<ISeeder, BeerSeeder>();

        return services;
    }
}
