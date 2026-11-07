using CartIQ.Application.Common.Models;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.Messaging;
using CartIQ.Application.OCR.Commands;
using MediatR;

namespace CartIQ.Application.Files.Commands.UploadFile;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Result<bool>>
{
    private readonly IFileUploadService _fileUploadService;
    private readonly IFileMetadataRepository _fileMetadataRepository;
    private readonly ISender _mediator;
    private readonly IEventPublisher _publisher;
    
    public UploadFileCommandHandler(IFileUploadService  fileUploadService, IFileMetadataRepository fileMetadataRepository, 
        ISender mediator,
        IEventPublisher publisher)
    {
        _publisher = publisher;
        _fileUploadService = fileUploadService;
        _fileMetadataRepository = fileMetadataRepository;
        _mediator = mediator;
    }
    
    public async Task<Result<bool>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();
        var fileExtension = Path.GetExtension(request.FileName);
        var newFileName = $"{fileId}{fileExtension}";
        
        // 1. Upload file to store
        var uploadFileResult = await _fileUploadService.DoUpload(newFileName, request.Content);
        
        // 2. Persist metadata DB record
        await _fileMetadataRepository.AddFileMetadata(fileId, request.ContentType, newFileName, cancellationToken);
        
        await _publisher.PublishFileUploadedEvent(fileId, cancellationToken);
        
        // 3. Return success
        return Result<bool>.Success(uploadFileResult);
    }
}