using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.Messaging;
using CartIQ.Application.OCR.Interfaces;
using CartIQ.Infrastructure.Data;
using CartIQ.Infrastructure.Data.Repositories;
using CartIQ.Infrastructure.FileUpload;
using CartIQ.Infrastructure.Messaging;
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

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            DatabaseConfiguration.Configure(options,  builder.Configuration);
        });
        
        builder.Services.AddScoped<ApplicationDbContextInitializer>();
        builder.Services.AddScoped<IReceiptsRepository, ReceiptsRepository>();
        builder.Services.AddScoped<IReceiptItemsRepository, ReceiptItemsRepository>();
        builder.Services.AddScoped<IFileUploadService, DiskFileUploadService>();
        builder.Services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();
        builder.Services.AddScoped<IRawOcrResultsRepository, RawOcrResultsRepository>();
        builder.Services.AddScoped<IEventPublisher, EventPublisher>();

        // Http clients
        builder.Services.AddHttpClient<IOcrService, McpOcrAPiOCRService>(client =>
        {
            client.BaseAddress = new Uri(
                builder.Configuration["services:ocr:http:0"]!);
        });
    }
}