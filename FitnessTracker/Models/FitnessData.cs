namespace FitnessTracker.Models;

/// <summary>
/// Root aggregate containing all user fitness data for JSON serialization.
/// </summary>
public class FitnessData
{
    private UserProfile _userProfile;
    private List<Exercise> _exercises;
    private List<Meal> _meals;
    private DateTime _lastModified;

    /// <summary>
    /// User's profile information.
    /// </summary>
    public UserProfile UserProfile
    {
        get => _userProfile;
        set => _userProfile = value;
    }

    /// <summary>
    /// Collection of all recorded exercises.
    /// </summary>
    public List<Exercise> Exercises
    {
        get => _exercises;
        set => _exercises = value;
    }

    /// <summary>
    /// Collection of all recorded meals.
    /// </summary>
    public List<Meal> Meals
    {
        get => _meals;
        set => _meals = value;
    }

    /// <summary>
    /// Timestamp of last data modification.
    /// </summary>
    public DateTime LastModified
    {
        get => _lastModified;
        set => _lastModified = value;
    }

    public FitnessData()
    {
        _userProfile = new UserProfile();
        _exercises = new List<Exercise>();
        _meals = new List<Meal>();
        _lastModified = DateTime.Now;
    }
}
