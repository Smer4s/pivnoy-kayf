using BeerBuzz.Domain;
using BeerBuzz.Domain.Abstractions;
using BeerBuzz.Domain.Common.Utils;
using BeerBuzz.Infrastructure;

namespace BeerBuzz.WebAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.ApplyDatabaseMigrations();

        app.Run();
    }

    private static void ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var publicMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
        publicMigrator!.Migrate();

        if (EnvFetcher.IsLocal() || EnvFetcher.IsDevelopment())
        {
            publicMigrator.ApplySeeds();
        }
    }
}
