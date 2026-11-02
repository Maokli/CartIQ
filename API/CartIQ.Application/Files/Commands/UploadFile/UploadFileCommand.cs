using CartIQ.Application.Common.Models;
using MediatR;
namespace CartIQ.Application.Files.Commands.UploadFile;

public class UploadFileCommand  : IRequest<Result<bool>>
{
    private byte[] _fileContent { get; }
    public MemoryStream Content => new MemoryStream(_fileContent, writable: false);
    public string ContentType { get; }
    public string FileName { get; }
    public long SizeInBytes { get; set; }

    public UploadFileCommand(byte[] content, string contentType, string fileName,  long sizeInBytes)
    {
        _fileContent = content;
        ContentType = contentType;
        FileName = fileName;
        SizeInBytes = sizeInBytes;
    }
}