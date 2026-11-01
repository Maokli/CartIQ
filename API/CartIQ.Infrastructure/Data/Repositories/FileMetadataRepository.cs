using Ardalis.GuardClauses;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Domain.Entities;
using CartIQ.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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
            State = FileState.Uploaded
        };

        var res = await _dbContext.FileMetadatas.AddAsync(metadataToAdd, cancellationToken);

        return res.Entity;
    }

    public async Task<bool> UpdateFileState(Guid fileId, FileState state, CancellationToken cancellationToken)
    {
        var fileMetadata = await _dbContext.FileMetadatas.SingleOrDefaultAsync(x => x.Id == fileId, cancellationToken);

        Guard.Against.NotFound(fileId, fileMetadata);

        fileMetadata.State = state;
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}