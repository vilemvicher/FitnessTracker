namespace FitnessTracker.Models;

/// <summary>
/// Contains user profile information used for calculations.
/// </summary>
public class UserProfile
{
    private string _name;
    private double _weightKg;
    private double _heightCm;
    private int _age;
    private Gender _gender;
    private WeeklyGoals _goals;

    /// <summary>
    /// User's display name.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// User's weight in kilograms.
    /// </summary>
    public double WeightKg
    {
        get => _weightKg;
        set => _weightKg = value;
    }

    /// <summary>
    /// User's height in centimeters.
    /// </summary>
    public double HeightCm
    {
        get => _heightCm;
        set => _heightCm = value;
    }

    /// <summary>
    /// User's age in years.
    /// </summary>
    public int Age
    {
        get => _age;
        set => _age = value;
    }

    /// <summary>
    /// User's biological gender for BMR calculation.
    /// </summary>
    public Gender Gender
    {
        get => _gender;
        set => _gender = value;
    }

    /// <summary>
    /// User's weekly fitness goals.
    /// </summary>
    public WeeklyGoals Goals
    {
        get => _goals;
        set => _goals = value;
    }

    public UserProfile()
    {
        _name = "User";
        _weightKg = 70.0;
        _heightCm = 170.0;
        _age = 30;
        _gender = Gender.Male;
        _goals = new WeeklyGoals();
    }
}
