using AutoMapper;
using CartIQ.Application.Common.Utils;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs.Mappers;

public class FileMetadataResolver: IValueResolver<Receipt, ReceiptDto, FileMetadataDto>
{
    public FileMetadataDto Resolve(Receipt source, ReceiptDto destination, FileMetadataDto destMember, ResolutionContext context)
    => new FileMetadataDto
        {
            State = source.FileMetadata.State,
            FileName = source.FileMetadata.FileName,
            ContentType = source.FileMetadata.ContentType,
        };
    
}