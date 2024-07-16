namespace BeerBuzz.Infrastructure;

public class InfrastructureConfiguration
{
    public FileStorageSettings FileStoreSettings { get; set; } = null!;
    public DbSettings DatabaseSettings { get; set; } = null!;
}

public record FileStorageSettings(string AccessKey, string SecretKey, string Endpoint);

public record DbSettings(string Host, string Port, string DatabaseName, string Username, string Password)
{
    public string ConnectionString => $"Host={Host};Port={Port};Database={DatabaseName};Username={Username};Password={Password};Include Error Detail=true";
}