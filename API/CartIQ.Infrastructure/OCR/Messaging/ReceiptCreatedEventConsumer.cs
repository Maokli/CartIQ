
using CartIQ.Application.Messaging;
using CartIQ.Application.OCR.Commands;
using CartIQ.Infrastructure.FileUpload.Messaging;
using MassTransit;
using MediatR;

namespace CartIQ.Infrastructure.OCR.Messaging;

public class ReceiptCreatedEventConsumer : IConsumer<ReceiptCreatedEvent>
{
    private readonly IMediator  _mediator;
    private readonly IEventPublisher _eventPublisher;
    
    public ReceiptCreatedEventConsumer(IMediator  mediator,  IEventPublisher eventPublisher)
    {
        _mediator = mediator;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Consume(ConsumeContext<ReceiptCreatedEvent> context)
    {
        var message = context.Message;
        var doOcrCommand = new DoOcrCommand(message.FileId, message.ReceiptId);
        
        // 1- Start OCR
        await _mediator.Send(doOcrCommand, context.CancellationToken);
        
        // 2- Publish OCRCompleted Event
        await _eventPublisher.PublishOcrCompletedEvent(message.ReceiptId, message.FileId, context.CancellationToken);
    }
}