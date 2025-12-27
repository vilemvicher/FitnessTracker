namespace FitnessTracker.Models;

/// <summary>
/// Represents a single exercise session recorded by the user.
/// </summary>
public class Exercise
{
    private Guid _id;
    private ExerciseType _type;
    private DateTime _date;
    private TimeSpan _duration;
    private double _caloriesBurned;
    private string _notes;

    /// <summary>
    /// Unique identifier for the exercise.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    /// <summary>
    /// Type of exercise performed.
    /// </summary>
    public ExerciseType Type
    {
        get => _type;
        set => _type = value;
    }

    /// <summary>
    /// Date and time when the exercise was performed.
    /// </summary>
    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    /// <summary>
    /// Duration of the exercise session.
    /// </summary>
    public TimeSpan Duration
    {
        get => _duration;
        set => _duration = value;
    }

    /// <summary>
    /// Total calories burned during the exercise.
    /// </summary>
    public double CaloriesBurned
    {
        get => _caloriesBurned;
        set => _caloriesBurned = value;
    }

    /// <summary>
    /// Optional notes about the exercise session.
    /// </summary>
    public string Notes
    {
        get => _notes;
        set => _notes = value;
    }

    public Exercise()
    {
        _id = Guid.NewGuid();
        _date = DateTime.Now;
        _notes = string.Empty;
    }
}
