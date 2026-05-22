using CartIQ.Application.Common.Interfaces;
using CartIQ.Infrastructure.Data;
using CartIQ.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartIQ.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        // TODO: SELECT BASED ON RUNTIME CONFIG
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        
        builder.Services.AddScoped<ApplicationDbContextInitializer>();
        builder.Services.AddScoped<IReceiptsRepository, ReceiptsRepository>();
        builder.Services.AddScoped<IReceiptItemsRepository, ReceiptItemsRepository>();
    }
}