using CartIQ.Application;
using CartIQ.Infrastructure;
using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Infrastructure.OCR.Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.AddMassTransitRabbitMq("message-broker", 
    massTransitConfiguration: registration =>
    {
        registration.AddConsumer<FIleUploadedEventConsumer>();
        registration.AddConsumer<ReceiptCreatedEventConsumer>();
    }
    );
builder.AddInfrastructureServices();
builder.AddApplicationServices();


var host = builder.Build();
host.Run();
