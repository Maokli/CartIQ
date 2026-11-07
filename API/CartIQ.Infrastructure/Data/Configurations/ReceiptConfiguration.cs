using CartIQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartIQ.Infrastructure.Data.Configurations;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.HasMany(r => r.Items)
            .WithOne(i => i.Receipt)
            .HasForeignKey(i => i.ReceiptId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(r => r.FileMetadata)
            .WithOne(fm => fm.Receipt)
            .HasForeignKey<Receipt>(r => r.FileMetadataId);
    }
}