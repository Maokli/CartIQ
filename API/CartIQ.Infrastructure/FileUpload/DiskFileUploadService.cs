using System.IO.Pipelines;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Infrastructure.FileUpload.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace CartIQ.Infrastructure.FileUpload;

public class DiskFileUploadService : IFileUploadService
{
    private const string FileUploadSection = "FileUpload";
    private const string UploadDirKey = "UploadDir";
    private readonly IPublishEndpoint _publishEndpoint;
    
    private readonly string? _uploadDir;
    
    public DiskFileUploadService(IConfiguration configuration, IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
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
    
    public async Task<bool> DoUpload(string fileName, Stream fileContent)
    {
        var filePath = Path.Combine(_uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            fileContent.CopyTo(fileStream);
        }

        var fileUploaded = new FileUploaded(fileName);
        await _publishEndpoint.Publish(fileUploaded);
        
        return true;
    }
}