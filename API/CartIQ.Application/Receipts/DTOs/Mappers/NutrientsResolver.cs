using AutoMapper;
using CartIQ.Application.Common.Models;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs.Mappers;


public class NutrientsResolver : IValueResolver<Receipt, ReceiptDto, NutrientsDto>
{
    public NutrientsDto Resolve(Receipt source, ReceiptDto destination, NutrientsDto destMember, ResolutionContext context)
    {
        var (totalCarbs, totalFat, totalProtein, totalEnergy) = GetTotalNutrients(source.Items);

        return new NutrientsDto()
        {
            Carbs = totalCarbs,
            Fat = totalFat,
            Protein = totalProtein,
            Kcal = totalEnergy
        };
    }

    private (double totalCarbs, double totalFat, double totalProtein, double totalEnergy) GetTotalNutrients(IReadOnlyCollection<ReceiptItem> items)
    {
        double totalCarbs = 0;
        double totalFat = 0;
        double totalProtein = 0;
        double totalEnergy = 0;
        
        foreach (var nutritionFact in items.Select(item => item.Product.Nutrition))
        {
            totalCarbs += nutritionFact.CarbohydratesPer100g ?? 0;
            totalFat += nutritionFact.FatPer100g ?? 0;
            totalProtein += nutritionFact.ProteinsPer100g ?? 0;
            totalEnergy += nutritionFact.EnergyKcalPer100g ?? 0;
        }
        
        return (totalCarbs, totalFat, totalProtein, totalEnergy);
    }
}