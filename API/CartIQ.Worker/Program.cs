using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.AddMassTransitRabbitMq("message-broker", 
    massTransitConfiguration: registration => registration.AddConsumer<FIleUploadedConsumer>()
    );

var host = builder.Build();
host.Run();
