namespace FitnessTracker.Models;

/// <summary>
/// Represents nutritional information for a meal or food item.
/// </summary>
public class NutritionInfo
{
    private double _calories;
    private double _proteinGrams;
    private double _carbohydratesGrams;
    private double _fatGrams;

    /// <summary>
    /// Total calories.
    /// </summary>
    public double Calories
    {
        get => _calories;
        set => _calories = value;
    }

    /// <summary>
    /// Protein content in grams.
    /// </summary>
    public double ProteinGrams
    {
        get => _proteinGrams;
        set => _proteinGrams = value;
    }

    /// <summary>
    /// Carbohydrates content in grams.
    /// </summary>
    public double CarbohydratesGrams
    {
        get => _carbohydratesGrams;
        set => _carbohydratesGrams = value;
    }

    /// <summary>
    /// Fat content in grams.
    /// </summary>
    public double FatGrams
    {
        get => _fatGrams;
        set => _fatGrams = value;
    }
}
