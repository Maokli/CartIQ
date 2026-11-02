using CartIQ.Domain.Entities;
using CartIQ.Domain.Enums;

namespace CartIQ.Application.Files.Interfaces;

public interface IFileMetadataRepository
{
    Task<FileMetadata> AddFileMetadata(Guid fileId, string contentType, string fileName, CancellationToken cancellationToken);
    Task<bool> UpdateFileState(Guid fileId, FileState state, CancellationToken cancellationToken);

}