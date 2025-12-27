namespace FitnessTracker.Models;

/// <summary>
/// Predefined exercise template with default calorie burn rate.
/// </summary>
public class ExercisePreset
{
    private ExerciseType _type;
    private string _name;
    private double _caloriesPerMinute;
    private string _description;

    /// <summary>
    /// Type of exercise.
    /// </summary>
    public ExerciseType Type
    {
        get => _type;
        set => _type = value;
    }

    /// <summary>
    /// Display name for the preset.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// Average calories burned per minute (based on 70kg person).
    /// </summary>
    public double CaloriesPerMinute
    {
        get => _caloriesPerMinute;
        set => _caloriesPerMinute = value;
    }

    /// <summary>
    /// Brief description of the exercise.
    /// </summary>
    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public ExercisePreset()
    {
        _name = string.Empty;
        _description = string.Empty;
    }
}
