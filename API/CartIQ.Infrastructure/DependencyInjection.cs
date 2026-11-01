using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.OCR.Interfaces;
using CartIQ.Infrastructure.Data;
using CartIQ.Infrastructure.Data.Repositories;
using CartIQ.Infrastructure.FileUpload;
using CartIQ.Infrastructure.OCR;
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
        builder.Services.AddScoped<IOcrService, DummyOCRService>();
        builder.Services.AddScoped<IFileUploadService, DiskFileUploadService>();
        builder.Services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();
        builder.Services.AddScoped<IRawOcrResultsRepository, RawOcrResultsRepository>();
    }
}