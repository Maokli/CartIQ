using CartIQ.Application;
using CartIQ.Infrastructure;
using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Infrastructure.OCR.Messaging;
using CartIQ.Infrastructure.ReceiptClassification.Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.AddMassTransitRabbitMq("message-broker", 
    massTransitConfiguration: registration =>
    {
        registration.AddConsumer<FIleUploadedEventConsumer>();
        registration.AddConsumer<ReceiptCreatedEventConsumer>();
        registration.AddConsumer<OcrCompletedEventConsumer>();
    }
    );
builder.AddInfrastructureServices();
builder.AddApplicationServices();


var host = builder.Build();
host.Run();
