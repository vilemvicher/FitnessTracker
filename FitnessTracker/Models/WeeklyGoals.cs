namespace FitnessTracker.Models;

/// <summary>
/// Defines user's weekly fitness targets.
/// </summary>
public class WeeklyGoals
{
    private double _caloriesBurnedTarget;
    private double _caloriesConsumedTarget;
    private int _exerciseCountTarget;

    /// <summary>
    /// Target calories to burn per week.
    /// </summary>
    public double CaloriesBurnedTarget
    {
        get => _caloriesBurnedTarget;
        set => _caloriesBurnedTarget = value;
    }

    /// <summary>
    /// Target calorie intake per week.
    /// </summary>
    public double CaloriesConsumedTarget
    {
        get => _caloriesConsumedTarget;
        set => _caloriesConsumedTarget = value;
    }

    /// <summary>
    /// Target number of exercise sessions per week.
    /// </summary>
    public int ExerciseCountTarget
    {
        get => _exerciseCountTarget;
        set => _exerciseCountTarget = value;
    }

    public WeeklyGoals()
    {
        _caloriesBurnedTarget = 2000;
        _caloriesConsumedTarget = 14000;
        _exerciseCountTarget = 5;
    }
}
