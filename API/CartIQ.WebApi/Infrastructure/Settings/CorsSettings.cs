namespace CartIQ.WebApi.Infrastructure.Settings;

public class CorsSettings
{
    public string[] AllowedOrigins { get; }
    public string[] AllowedMethods { get; }
    public string[] AllowedHeaders { get; }
}