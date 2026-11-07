using CartIQ.Domain.Common;
using CartIQ.Domain.Enums;

namespace CartIQ.Domain.Entities;

public class FileMetadata : BaseAuditableEntity
{
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public FileState State { get; set; }
    public Receipt Receipt { get; set; }
}