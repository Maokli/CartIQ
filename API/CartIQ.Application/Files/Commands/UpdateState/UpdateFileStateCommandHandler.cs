using CartIQ.Application.Common.Models;
using CartIQ.Application.Files.Interfaces;
using MediatR;

namespace CartIQ.Application.Files.Commands.UpdateState;

public class UpdateFileStateCommandHandler : IRequestHandler<UpdateFileStateCommand, Result<bool>>
{    
    private readonly IFileMetadataRepository _fileMetadataRepository;
    
    public UpdateFileStateCommandHandler(IFileMetadataRepository fileMetadataRepository)
    {
        _fileMetadataRepository = fileMetadataRepository;
    }
    
    public async Task<Result<bool>> Handle(UpdateFileStateCommand request, CancellationToken cancellationToken)
    {
        var result = await _fileMetadataRepository.UpdateFileState(request.FileId, request.State, cancellationToken);
        
        return Result<bool>.Success(result);
    }
}