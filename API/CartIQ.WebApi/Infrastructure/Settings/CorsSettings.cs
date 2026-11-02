namespace CartIQ.WebApi.Infrastructure.Settings;

public class CorsSettings
{
    public string[] AllowedOrigins { get; init; } = [];
    public string[] AllowedMethods { get; init; } = [];
    public string[] AllowedHeaders { get; init; } = [];
}