using Ardalis.GuardClauses;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Domain.Entities;
using CartIQ.Domain.Enums;
using CartIQ.Infrastructure.Data.Repositories.Shared;

namespace CartIQ.Infrastructure.Data.Repositories;

public class FileMetadataRepository : IFileMetadataRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FileMetadataRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileMetadata> AddFileMetadata(Guid fileId, string contentType, string fileName, CancellationToken cancellationToken)
    {
        var metadataToAdd = new FileMetadata()
        {
            Id = fileId,
            ContentType = contentType,
            FileName = fileName,
            State = FileState.Uploaded,
            CreatedBy = "louay", // TODO: Fix here
            Created =  DateTime.UtcNow,
            LastModifiedBy = "louay",
            LastModified =  DateTime.UtcNow,
        };
        
        _dbContext.FileMetadatas.Add(metadataToAdd);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return metadataToAdd;
    }

    public async Task<bool> UpdateFileState(Guid fileId, FileState state, CancellationToken cancellationToken)
    {
        await _dbContext.UpdateFileStateAsync(fileId, state, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

}