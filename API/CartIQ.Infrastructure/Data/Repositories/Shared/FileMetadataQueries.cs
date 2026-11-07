using Ardalis.GuardClauses;
using CartIQ.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Infrastructure.Data.Repositories.Shared;

public static class FileMetadataQueries
{
    public static async Task UpdateFileStateAsync(this ApplicationDbContext dbContext,  
        Guid fileId, 
        FileState state, 
        CancellationToken cancellationToken)
    {
        var fileMetadata = await dbContext.FileMetadatas.SingleOrDefaultAsync(x => x.Id == fileId, cancellationToken);

        Guard.Against.NotFound(fileId, fileMetadata);

        fileMetadata.State = state;
    }
}