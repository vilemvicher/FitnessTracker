using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services;

/// <summary>
/// Service for calculating fitness statistics.
/// </summary>
public class StatisticsService : IStatisticsService
{
    private readonly IDataService _dataService;

    public StatisticsService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public int GetTotalExerciseCount()
    {
        return _dataService.Data.Exercises.Count;
    }

    public double GetTotalCaloriesBurned()
    {
        return _dataService.Data.Exercises.Sum(e => e.CaloriesBurned);
    }

    public double GetCaloriesBurnedInPeriod(DateTime start, DateTime end)
    {
        return _dataService.Data.Exercises
            .Where(e => e.Date.Date >= start.Date && e.Date.Date <= end.Date)
            .Sum(e => e.CaloriesBurned);
    }

    public TimeSpan GetAverageTrainingDuration()
    {
        var exercises = _dataService.Data.Exercises;
        if (exercises.Count == 0)
            return TimeSpan.Zero;

        var totalTicks = exercises.Sum(e => e.Duration.Ticks);
        return TimeSpan.FromTicks(totalTicks / exercises.Count);
    }

    public ExerciseType? GetMostFrequentExerciseType()
    {
        var exercises = _dataService.Data.Exercises;
        if (exercises.Count == 0)
            return null;

        return exercises
            .GroupBy(e => e.Type)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;
    }

    public double GetTotalCaloriesConsumed()
    {
        return _dataService.Data.Meals.Sum(m => m.Nutrition.Calories * m.ServingSize);
    }

    public double GetCaloriesConsumedInPeriod(DateTime start, DateTime end)
    {
        return _dataService.Data.Meals
            .Where(m => m.Date.Date >= start.Date && m.Date.Date <= end.Date)
            .Sum(m => m.Nutrition.Calories * m.ServingSize);
    }

    public double GetWeeklyBurnedGoalProgress()
    {
        var weekStart = GetStartOfCurrentWeek();
        var weekEnd = weekStart.AddDays(7);
        var burned = GetCaloriesBurnedInPeriod(weekStart, weekEnd);
        var target = _dataService.Data.UserProfile.Goals.CaloriesBurnedTarget;

        if (target <= 0)
            return 0;

        return Math.Min(100, (burned / target) * 100);
    }

    public double GetWeeklyConsumedGoalProgress()
    {
        var weekStart = GetStartOfCurrentWeek();
        var weekEnd = weekStart.AddDays(7);
        var consumed = GetCaloriesConsumedInPeriod(weekStart, weekEnd);
        var target = _dataService.Data.UserProfile.Goals.CaloriesConsumedTarget;

        if (target <= 0)
            return 0;

        return Math.Min(100, (consumed / target) * 100);
    }

    public double GetWeeklyExerciseCountProgress()
    {
        var weekStart = GetStartOfCurrentWeek();
        var weekEnd = weekStart.AddDays(7);
        var count = _dataService.Data.Exercises
            .Count(e => e.Date.Date >= weekStart.Date && e.Date.Date <= weekEnd.Date);
        var target = _dataService.Data.UserProfile.Goals.ExerciseCountTarget;

        if (target <= 0)
            return 0;

        return Math.Min(100, ((double)count / target) * 100);
    }

    private static DateTime GetStartOfCurrentWeek()
    {
        var today = DateTime.Today;
        var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        return today.AddDays(-diff);
    }
}
