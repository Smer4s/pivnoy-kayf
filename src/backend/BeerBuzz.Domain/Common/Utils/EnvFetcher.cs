namespace BeerBuzz.Domain.Common.Utils;

public static class EnvFetcher
{
    public static string CurrentEnvironment => GetRequiredEnvVariable("ASPNETCORE_ENVIRONMENT");

    public static string GetRequiredEnvVariable(string variableName)
    {
        return GetEnvVariable(variableName)
               ?? throw new NullReferenceException($"No {variableName} env variable provided");
    }

    public static string GetDatabaseConnection()
    {
        var dbHost = GetRequiredEnvVariable("DATABASE_HOST");
        var dbPort = GetRequiredEnvVariable("DATABASE_PORT");
        var dbName = GetRequiredEnvVariable("DATABASE_NAME");
        var dbUsername = GetRequiredEnvVariable("DATABASE_USERNAME");
        var dbPassword = GetRequiredEnvVariable("DATABASE_PASSWORD");

        return
            $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUsername};Password={dbPassword};Include Error Detail=true";
    }

    public static bool IsLocal()
    {
        return CurrentEnvironment == "Local";
    }

    public static bool IsDevelopment()
    {
        return CurrentEnvironment == "Development";
    }

    public static bool IsProduction()
    {
        return CurrentEnvironment == "Production";
    }

    public static bool IsTesting()
    {
        return CurrentEnvironment == "Testing";
    }

    private static string? GetEnvVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName);
    }
}
