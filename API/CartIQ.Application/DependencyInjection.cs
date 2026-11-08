using System.Reflection;
using CartIQ.Application.Common.Behaviors;
using CartIQ.Application.Files;
using CartIQ.Application.ReceiptClassification;
using CartIQ.Application.ReceiptClassification.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartIQ.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var fileSettings = builder.Configuration.GetSection("FileSettings").Get<FileSettings>()!;
        builder.Services.AddSingleton(fileSettings);
        
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Services.AddAutoMapper(cfg => 
            cfg.AddMaps(Assembly.GetExecutingAssembly()), Assembly.GetExecutingAssembly());
        
        builder.Services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
                .AddOpenBehavior(typeof(ValidationBehavior<,>))
                .AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>))
                .AddOpenBehavior(typeof(LoggingBehavior<,>)));

        builder.Services.AddScoped<IReceiptClassificationService, OllamaReceiptClassificationService>();
    }
}