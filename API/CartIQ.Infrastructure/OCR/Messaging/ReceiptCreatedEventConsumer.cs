using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.Messaging;
using CartIQ.Application.OCR.Commands;
using CartIQ.Domain.Enums;
using CartIQ.Infrastructure.Data.Repositories;
using CartIQ.Infrastructure.FileUpload.Messaging;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.OCR.Messaging;

public class ReceiptCreatedEventConsumer : IConsumer<ReceiptCreatedEvent>
{
    private readonly IMediator  _mediator;
    
    public ReceiptCreatedEventConsumer(IMediator  mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Consume(ConsumeContext<ReceiptCreatedEvent> context)
    {
        var message = context.Message;
        var doOcrCommand = new DoOcrCommand(message.FileId, message.ReceiptId);
        
        // 1- Start OCR
        await _mediator.Send(doOcrCommand);
    }
}