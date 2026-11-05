using MassTransit;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.FileUpload.Messaging;

public sealed record FileUploaded(string fileName);

public class FIleUploadedConsumer : IConsumer<FileUploaded>
{
    private readonly ILogger<FIleUploadedConsumer> _logger;
    public FIleUploadedConsumer(ILogger<FIleUploadedConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<FileUploaded> context)
    {
        var message = context.Message;
        _logger.LogInformation("File Uploaded consumer started! Name: {fileName}", message.fileName);
        
        return Task.CompletedTask;
    }
}