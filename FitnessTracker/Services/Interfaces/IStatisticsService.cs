using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces;

/// <summary>
/// Service for calculating fitness statistics.
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// Gets total number of recorded exercises.
    /// </summary>
    int GetTotalExerciseCount();

    /// <summary>
    /// Gets total calories burned across all exercises.
    /// </summary>
    double GetTotalCaloriesBurned();

    /// <summary>
    /// Gets calories burned within a date range.
    /// </summary>
    double GetCaloriesBurnedInPeriod(DateTime start, DateTime end);

    /// <summary>
    /// Gets average training duration across all exercises.
    /// </summary>
    TimeSpan GetAverageTrainingDuration();

    /// <summary>
    /// Gets the most frequently performed exercise type.
    /// </summary>
    ExerciseType? GetMostFrequentExerciseType();

    /// <summary>
    /// Gets total calories consumed across all meals.
    /// </summary>
    double GetTotalCaloriesConsumed();

    /// <summary>
    /// Gets calories consumed within a date range.
    /// </summary>
    double GetCaloriesConsumedInPeriod(DateTime start, DateTime end);

    /// <summary>
    /// Gets weekly burned calories goal progress (0-100%).
    /// </summary>
    double GetWeeklyBurnedGoalProgress();

    /// <summary>
    /// Gets weekly consumed calories goal progress (0-100%).
    /// </summary>
    double GetWeeklyConsumedGoalProgress();

    /// <summary>
    /// Gets weekly exercise count goal progress (0-100%).
    /// </summary>
    double GetWeeklyExerciseCountProgress();
}
