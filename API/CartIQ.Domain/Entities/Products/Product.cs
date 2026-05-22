using CartIQ.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Domain.Entities.Products;

public class Product : BaseAuditableEntity
{
    public string Barcode { get; set; }
    public string Name { get; set; }
    public List<string> Brands { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    public string FoodGroup { get; set; }
    public NutritionFacts Nutrition { get; set; }
    public bool HasNutritionData { get; set; }
    public string NutritionDataPer { get; set; }
    public string Source { get; set; }
}

[Owned]
public class NutritionFacts
{
    public double? EnergyKcalPer100g { get; set; }
    public double? EnergyKjPer100g { get; set; }
    public double? FatPer100g { get; set; }
    public double? SaturatedFatPer100g { get; set; }
    public double? CarbohydratesPer100g { get; set; }
    public double? SugarsPer100g { get; set; }
    public double? ProteinsPer100g { get; set; }
    public double? SaltPer100g { get; set; }
    public double? SodiumPer100g { get; set; }
}