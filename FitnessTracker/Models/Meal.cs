namespace FitnessTracker.Models;

/// <summary>
/// Represents a meal or food item consumed by the user.
/// </summary>
public class Meal
{
    private Guid _id;
    private string _name;
    private DateTime _date;
    private NutritionInfo _nutrition;
    private double _servingSize;
    private string _notes;

    /// <summary>
    /// Unique identifier for the meal.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    /// <summary>
    /// Name of the meal or food item.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// Date and time when the meal was consumed.
    /// </summary>
    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    /// <summary>
    /// Nutritional information for this meal.
    /// </summary>
    public NutritionInfo Nutrition
    {
        get => _nutrition;
        set => _nutrition = value;
    }

    /// <summary>
    /// Serving size multiplier (1.0 = standard serving).
    /// </summary>
    public double ServingSize
    {
        get => _servingSize;
        set => _servingSize = value;
    }

    /// <summary>
    /// Optional notes about the meal.
    /// </summary>
    public string Notes
    {
        get => _notes;
        set => _notes = value;
    }

    public Meal()
    {
        _id = Guid.NewGuid();
        _date = DateTime.Now;
        _name = string.Empty;
        _notes = string.Empty;
        _nutrition = new NutritionInfo();
        _servingSize = 1.0;
    }
}
