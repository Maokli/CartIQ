using MediatR;

namespace CartIQ.Application.OCR.Commands;

public class DoOcrCommand : IRequest
{
    public Guid FileId { get; set; }
    public MemoryStream FileContent { get; set; }

    public DoOcrCommand(Guid fileId, MemoryStream fileContent)
    {
        FileId = fileId;
        FileContent = fileContent;
    }
}