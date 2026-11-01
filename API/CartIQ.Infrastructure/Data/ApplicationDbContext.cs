using System.Reflection;
using CartIQ.Domain.Entities;
using CartIQ.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<RawOcrResult> RawOcrResults { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public DbSet<FileMetadata> FileMetadatas { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}