using System.Text.Json;
using CartIQ.Domain.Entities;
using CartIQ.Domain.Entities.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitialiseAsync();
    }
}

internal class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly IHostEnvironment _env;

    public ApplicationDbContextInitializer(ApplicationDbContext context,  ILogger<ApplicationDbContextInitializer> logger, IHostEnvironment env)
    {
        _context = context;
        _logger = logger;
        _env = env;
    }
    
    
    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await SeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    private async Task SeedAsync()
    {
        try
        {
            if (await _context.Receipts.AnyAsync())
            {
                _logger.LogInformation("Database already contains data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding database from seedData.json...");

            //await ApplicationDbContextInitializerSeeder.SeedFromJsonAsync(_context, _logger, _env);

            _logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}

internal class ApplicationDbContextInitializerSeeder
{
    // public static async Task SeedFromJsonAsync(ApplicationDbContext context, ILogger logger, IWebHostEnvironment env)
    // {
    //     var jsonFilePath = Path.Combine(env.ContentRootPath, "SeedData.json");
    //
    //     if (!File.Exists(jsonFilePath))
    //     {
    //         logger.LogWarning("seedData.json not found at {Path}. Skipping seed.", jsonFilePath);
    //         return;
    //     }
    //
    //     await using var stream = File.OpenRead(jsonFilePath);
    //     var seedData = await JsonSerializer.DeserializeAsync<SeedData>(stream, new JsonSerializerOptions
    //     {
    //         PropertyNameCaseInsensitive = true
    //     });
    //
    //     if (seedData?.Receipts is null or { Count: 0 })
    //     {
    //         logger.LogWarning("Seed file is empty or invalid. Skipping seeding.");
    //         return;
    //     }
    //
    //     var now = DateTimeOffset.UtcNow;
    //     const string seededBy = "system-seed";
    //
    //     // --- Products ---
    //     var productsByBarcode = new Dictionary<string, Product>();
    //
    //     if (seedData.Products is { Count: > 0 })
    //     {
    //         foreach (var dto in seedData.Products)
    //         {
    //             var product = new Product
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Barcode = dto.Barcode,
    //                 Name = dto.Name,
    //                 Brands = dto.Brands,
    //                 Categories = dto.Categories,
    //                 FoodGroup = dto.FoodGroup,
    //                 Source = dto.Source,
    //                 HasNutritionData = dto.HasNutritionData,
    //                 NutritionDataPer = dto.NutritionDataPer,
    //                 Nutrition = new NutritionFacts
    //                 {
    //                     EnergyKcalPer100g    = dto.Nutrition.EnergyKcalPer100g,
    //                     EnergyKjPer100g      = dto.Nutrition.EnergyKjPer100g,
    //                     FatPer100g           = dto.Nutrition.FatPer100g,
    //                     SaturatedFatPer100g  = dto.Nutrition.SaturatedFatPer100g,
    //                     CarbohydratesPer100g = dto.Nutrition.CarbohydratesPer100g,
    //                     SugarsPer100g        = dto.Nutrition.SugarsPer100g,
    //                     ProteinsPer100g      = dto.Nutrition.ProteinsPer100g,
    //                     SaltPer100g          = dto.Nutrition.SaltPer100g,
    //                     SodiumPer100g        = dto.Nutrition.SodiumPer100g
    //                 },
    //                 Created = now, LastModified = now, CreatedBy = seededBy, LastModifiedBy = seededBy
    //             };
    //
    //             productsByBarcode[dto.Barcode] = product;
    //         }
    //
    //         await context.Products.AddRangeAsync(productsByBarcode.Values);
    //     }
    //
    //     // --- Receipts + Items ---
    //     foreach (var dto in seedData.Receipts)
    //     {
    //         var receipt = new Receipt(Guid.NewGuid(), dto.Merchant, dto.Currency)
    //         {
    //             RawOcrResult = new RawOcrResult
    //             {
    //                 Id = Guid.NewGuid(),
    //                 Result = dto.RawOcrResult,
    //                 Created = now, LastModified = now, CreatedBy = seededBy, LastModifiedBy = seededBy
    //             },
    //             Created = now, LastModified = now, CreatedBy = seededBy, LastModifiedBy = seededBy
    //         };
    //
    //         await context.Receipts.AddAsync(receipt);
    //
    //         var items = dto.Items.Select(i =>
    //         {
    //             var item = new ReceiptItem(receipt.Id, i.ProductName, i.ProductPrice)
    //             {
    //                 Created = now, LastModified = now, CreatedBy = seededBy, LastModifiedBy = seededBy
    //             };
    //
    //             if (i.ProductBarcode is not null &&
    //                 productsByBarcode.TryGetValue(i.ProductBarcode, out var product))
    //             {
    //                 item.ProductId = product.Id;
    //                 item.Product = product;
    //             }
    //             else
    //             {
    //                 logger.LogWarning(
    //                     "No product found for barcode '{Barcode}' on item '{Item}'. Skipping product link.",
    //                     i.ProductBarcode, i.ProductName);
    //             }
    //
    //             return item;
    //         });
    //
    //         await context.ReceiptItems.AddRangeAsync(items);
    //     }
    //
    //     await context.SaveChangesAsync();
    //     logger.LogInformation("Database seeded successfully from {Path}.", jsonFilePath);
    // }
    // // DTOs used only for deserialization
    // internal record SeedData(List<ProductSeedDto> Products, List<ReceiptSeedDto> Receipts);
    //
    // internal record ProductSeedDto(
    //     string Barcode,
    //     string Name,
    //     List<string> Brands,
    //     List<string> Categories,
    //     string? FoodGroup,
    //     string Source,
    //     bool HasNutritionData,
    //     string? NutritionDataPer,
    //     NutritionFactsSeedDto Nutrition);
    //
    // internal record NutritionFactsSeedDto(
    //     double? EnergyKcalPer100g,
    //     double? EnergyKjPer100g,
    //     double? FatPer100g,
    //     double? SaturatedFatPer100g,
    //     double? CarbohydratesPer100g,
    //     double? SugarsPer100g,
    //     double? ProteinsPer100g,
    //     double? SaltPer100g,
    //     double? SodiumPer100g);
    //
    // internal record ReceiptSeedDto(string Merchant, string Currency, string RawOcrResult, List<ReceiptItemSeedDto> Items);
    //
    // internal record ReceiptItemSeedDto(string ProductName, double ProductPrice, string? ProductBarcode);
}