using System.ComponentModel.DataAnnotations.Schema;
using CartIQ.Domain.Common;

namespace CartIQ.Domain.Entities;

public class Receipt : BaseAuditableEntity
{
    public string Merchant { get; set; }
    public string Currency { get; set; }
    public IReadOnlyCollection<ReceiptItem> Items { get; set; }
    public RawOcrResult RawOcrResult { get; set; }
    
    [NotMapped]
    public double TotalAmount => Items.Sum(item => item.ProductPrice);

    public Receipt(Guid id, string  merchant, string currency)
    {
        if (string.IsNullOrEmpty(merchant))
        {
            throw new ArgumentNullException(nameof(merchant));
        }
        if (string.IsNullOrEmpty(currency))
        {
            throw new ArgumentNullException(nameof(currency));
        }

        if (id.Equals(Guid.Empty))
        {
            throw new ArgumentNullException(nameof(id));
        }
        
        Id = id;
        Merchant = merchant;
        Currency = currency.ToUpper();
    }

    public Receipt(Guid id, string  merchant, string currency, IReadOnlyCollection<ReceiptItem> items, RawOcrResult rawOcrResult)
        : this(id, merchant, currency)
    {
        Items = items;
        RawOcrResult = rawOcrResult;
    }
}