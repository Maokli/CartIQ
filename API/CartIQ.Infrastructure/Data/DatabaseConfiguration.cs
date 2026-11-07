
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CartIQ.Infrastructure.Data;

public static class DatabaseConfiguration
{
    public static void Configure(
        DbContextOptionsBuilder options,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string is missing.");

        options.UseSqlite(connectionString);
    }
}