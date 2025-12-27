namespace FitnessTracker.Models;

/// <summary>
/// Predefined meal template with nutrition information.
/// </summary>
public class MealPreset
{
    private string _name;
    private string _category;
    private NutritionInfo _nutrition;
    private string _description;

    /// <summary>
    /// Name of the preset meal.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// Category (Breakfast, Lunch, Dinner, Snack).
    /// </summary>
    public string Category
    {
        get => _category;
        set => _category = value;
    }

    /// <summary>
    /// Default nutritional information per serving.
    /// </summary>
    public NutritionInfo Nutrition
    {
        get => _nutrition;
        set => _nutrition = value;
    }

    /// <summary>
    /// Brief description of the meal.
    /// </summary>
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public MealPreset()
    {
        _name = string.Empty;
        _category = string.Empty;
        _description = string.Empty;
        _nutrition = new NutritionInfo();
    }
}
