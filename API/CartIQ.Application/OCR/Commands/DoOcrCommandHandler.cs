using CartIQ.Application.Files.Commands.UpdateState;
using CartIQ.Application.OCR.Interfaces;
using CartIQ.Domain.Enums;
using MediatR;

namespace CartIQ.Application.OCR.Commands;

public class DoOcrCommandHandler : IRequestHandler<DoOcrCommand>
{
    private readonly IOcrService _ocrService;
    private IRawOcrResultsRepository _rawOcrResultsRepository;
    private ISender _mediator;
    
    public DoOcrCommandHandler(IOcrService ocrService, IRawOcrResultsRepository rawOcrResultsRepository, ISender mediator)
    {
        _ocrService = ocrService;    
        _rawOcrResultsRepository = rawOcrResultsRepository;
        _mediator = mediator;
    }
    
    public async Task Handle(DoOcrCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new UpdateFileStateCommand(request.FileId, FileState.OcrStarted));
            var ocrResult = await _ocrService.DoOcr(request.FileContent);
            
            await _rawOcrResultsRepository.AddRawOcrResult(request.FileId, ocrResult, cancellationToken);
            await _mediator.Send(new UpdateFileStateCommand(request.FileId, FileState.OcrCompleted));
        }
        catch (Exception e)
        {
            await _mediator.Send(new UpdateFileStateCommand(request.FileId, FileState.OcrError));
            throw;
        }
    }
}