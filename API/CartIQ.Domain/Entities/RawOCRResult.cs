using CartIQ.Domain.Common;

namespace CartIQ.Domain.Entities;

public class RawOcrResult : BaseAuditableEntity
{
    public string Result { get; set; }
}