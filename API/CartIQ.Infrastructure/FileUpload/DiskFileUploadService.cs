using System.IO.Pipelines;
using Ardalis.GuardClauses;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Infrastructure.FileUpload.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace CartIQ.Infrastructure.FileUpload;

public class DiskFileUploadService : IFileUploadService
{
    private const string FileUploadSection = "FileUpload";
    private const string UploadDirKey = "UploadDir";
    
    private readonly string? _uploadDir;
    
    public DiskFileUploadService(IConfiguration configuration)
    {
        _uploadDir = configuration.GetSection(FileUploadSection)[UploadDirKey];

        if (string.IsNullOrWhiteSpace(_uploadDir))
        {
            throw new ArgumentNullException(nameof(_uploadDir));
        }
        
        if (!Directory.Exists(_uploadDir))
        {
            Directory.CreateDirectory(_uploadDir);
        }
    }
    
    public Task<bool> DoUpload(string fileName, Stream fileContent)
    {
        var filePath = Path.Combine(_uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            fileContent.CopyTo(fileStream);
        }
        
        return Task.FromResult(true);
    }
    
    public async Task<Stream> GetFile(
        string fileName,
        CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_uploadDir, fileName);

        if (!File.Exists(filePath))
            throw new NotFoundException("File not found", filePath);

        return await Task.FromResult<Stream>(
            new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 81920,
                useAsync: true));
    }
}