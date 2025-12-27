using FitnessTracker.Models;

namespace FitnessTracker.Services;

/// <summary>
/// Calculates calorie burn and daily calorie needs based on user profile.
/// </summary>
public class CalorieCalculator
{
    private const double BaseWeightKg = 70.0;

    /// <summary>
    /// Calculates calories burned adjusting for user's weight.
    /// Base rate is for 70kg person, adjusted proportionally by weight.
    /// </summary>
    /// <param name="preset">Exercise preset with base calorie rate.</param>
    /// <param name="durationMinutes">Duration in minutes.</param>
    /// <param name="userWeightKg">User's weight in kilograms.</param>
    /// <returns>Estimated calories burned.</returns>
    public double CalculateCaloriesBurned(ExercisePreset preset, int durationMinutes, double userWeightKg)
    {
        var weightFactor = userWeightKg / BaseWeightKg;
        return preset.CaloriesPerMinute * durationMinutes * weightFactor;
    }

    /// <summary>
    /// Calculates calories burned using a direct calorie-per-minute rate.
    /// </summary>
    /// <param name="caloriesPerMinute">Calories burned per minute.</param>
    /// <param name="durationMinutes">Duration in minutes.</param>
    /// <param name="userWeightKg">User's weight in kilograms.</param>
    /// <returns>Estimated calories burned.</returns>
    public double CalculateCaloriesBurned(double caloriesPerMinute, int durationMinutes, double userWeightKg)
    {
        var weightFactor = userWeightKg / BaseWeightKg;
        return caloriesPerMinute * durationMinutes * weightFactor;
    }

    /// <summary>
    /// Calculates Basal Metabolic Rate using Mifflin-St Jeor equation.
    /// BMR is the number of calories your body needs at complete rest.
    /// </summary>
    /// <param name="weightKg">Weight in kilograms.</param>
    /// <param name="heightCm">Height in centimeters.</param>
    /// <param name="age">Age in years.</param>
    /// <param name="gender">Biological gender.</param>
    /// <returns>BMR in calories per day.</returns>
    public double CalculateBMR(double weightKg, double heightCm, int age, Gender gender)
    {
        // Mifflin-St Jeor Equation:
        // Men:   BMR = (10 × weight in kg) + (6.25 × height in cm) − (5 × age in years) + 5
        // Women: BMR = (10 × weight in kg) + (6.25 × height in cm) − (5 × age in years) − 161

        var baseBmr = (10 * weightKg) + (6.25 * heightCm) - (5 * age);

        return gender == Gender.Male
            ? baseBmr + 5
            : baseBmr - 161;
    }

    /// <summary>
    /// Calculates Total Daily Energy Expenditure based on activity level.
    /// TDEE is the total calories burned per day including activity.
    /// </summary>
    /// <param name="bmr">Basal Metabolic Rate.</param>
    /// <param name="activityMultiplier">Activity level multiplier (1.2-1.9).</param>
    /// <returns>TDEE in calories per day.</returns>
    public double CalculateTDEE(double bmr, double activityMultiplier)
    {
        return bmr * activityMultiplier;
    }

    /// <summary>
    /// Gets recommended daily calories based on user profile.
    /// Uses moderate activity level (1.55 multiplier).
    /// </summary>
    /// <param name="profile">User profile with weight, height, age, gender.</param>
    /// <returns>Recommended daily calorie intake.</returns>
    public double GetRecommendedDailyCalories(UserProfile profile)
    {
        var bmr = CalculateBMR(profile.WeightKg, profile.HeightCm, profile.Age, profile.Gender);
        // Moderate activity level (exercise 3-5 days/week)
        return CalculateTDEE(bmr, 1.55);
    }

    /// <summary>
    /// Gets the activity level description.
    /// </summary>
    public static string GetActivityLevelDescription(double multiplier)
    {
        return multiplier switch
        {
            <= 1.2 => "Sedentary (little or no exercise)",
            <= 1.375 => "Lightly active (light exercise 1-3 days/week)",
            <= 1.55 => "Moderately active (moderate exercise 3-5 days/week)",
            <= 1.725 => "Very active (hard exercise 6-7 days/week)",
            _ => "Extra active (very hard exercise & physical job)"
        };
    }
}
