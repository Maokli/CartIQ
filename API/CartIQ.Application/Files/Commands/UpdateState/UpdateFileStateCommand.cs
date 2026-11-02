using CartIQ.Application.Common.Models;
using CartIQ.Domain.Enums;
using MediatR;

namespace CartIQ.Application.Files.Commands.UpdateState;

public class UpdateFileStateCommand : IRequest<Result<bool>>
{
    public Guid FileId { get; set; }
    public FileState State { get; set; }

    public UpdateFileStateCommand(Guid fileId, FileState state)
    {
        FileId = fileId;
        State = state;
    }
}