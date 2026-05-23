using CartIQ.Domain.Common;
using CartIQ.Domain.Entities.Products;

namespace CartIQ.Domain.Entities;

public class ReceiptItem : BaseAuditableEntity
{
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Guid ReceiptId { get; set; }
    public Receipt Receipt { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public ReceiptItem(Guid receiptId, string productName, double productPrice)
    {
        if (string.IsNullOrEmpty(productName))
        {
            throw new ArgumentNullException(nameof(productName));
        }
        
        if (receiptId.Equals(Guid.Empty))
        {
            throw new ArgumentNullException(nameof(receiptId));
        }
        
        ProductName = productName;
        ProductPrice = productPrice;
        ReceiptId = receiptId;
    }
}