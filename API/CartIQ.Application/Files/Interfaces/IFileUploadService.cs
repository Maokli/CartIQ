namespace CartIQ.Application.Files.Interfaces;

public interface IFileUploadService
{
    Task<bool> DoUpload(string fileName, Stream fileContent);
    Task<Stream> GetFile(string fileName, CancellationToken cancellationToken);
}