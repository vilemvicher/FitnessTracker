using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for displaying comprehensive fitness statistics.
/// </summary>
public class StatisticsViewModel : ViewModelBase
{
    private readonly IStatisticsService _statisticsService;

    private int _totalExerciseCount;
    private double _totalCaloriesBurned;
    private double _weeklyCaloriesBurned;
    private double _monthlyCaloriesBurned;
    private string _averageTrainingDuration;
    private string _mostFrequentExerciseType;

    private double _totalCaloriesConsumed;
    private double _weeklyCaloriesConsumed;
    private double _monthlyCaloriesConsumed;

    private double _weeklyBurnedGoalProgress;
    private double _weeklyConsumedGoalProgress;
    private double _weeklyExerciseCountProgress;

    public int TotalExerciseCount
    {
        get => _totalExerciseCount;
        set => SetProperty(ref _totalExerciseCount, value);
    }

    public double TotalCaloriesBurned
    {
        get => _totalCaloriesBurned;
        set => SetProperty(ref _totalCaloriesBurned, value);
    }

    public double WeeklyCaloriesBurned
    {
        get => _weeklyCaloriesBurned;
        set => SetProperty(ref _weeklyCaloriesBurned, value);
    }

    public double MonthlyCaloriesBurned
    {
        get => _monthlyCaloriesBurned;
        set => SetProperty(ref _monthlyCaloriesBurned, value);
    }

    public string AverageTrainingDuration
    {
        get => _averageTrainingDuration;
        set => SetProperty(ref _averageTrainingDuration, value);
    }

    public string MostFrequentExerciseType
    {
        get => _mostFrequentExerciseType;
        set => SetProperty(ref _mostFrequentExerciseType, value);
    }

    public double TotalCaloriesConsumed
    {
        get => _totalCaloriesConsumed;
        set => SetProperty(ref _totalCaloriesConsumed, value);
    }

    public double WeeklyCaloriesConsumed
    {
        get => _weeklyCaloriesConsumed;
        set => SetProperty(ref _weeklyCaloriesConsumed, value);
    }

    public double MonthlyCaloriesConsumed
    {
        get => _monthlyCaloriesConsumed;
        set => SetProperty(ref _monthlyCaloriesConsumed, value);
    }

    public double WeeklyBurnedGoalProgress
    {
        get => _weeklyBurnedGoalProgress;
        set => SetProperty(ref _weeklyBurnedGoalProgress, value);
    }

    public double WeeklyConsumedGoalProgress
    {
        get => _weeklyConsumedGoalProgress;
        set => SetProperty(ref _weeklyConsumedGoalProgress, value);
    }

    public double WeeklyExerciseCountProgress
    {
        get => _weeklyExerciseCountProgress;
        set => SetProperty(ref _weeklyExerciseCountProgress, value);
    }

    public StatisticsViewModel(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
        _averageTrainingDuration = "0 min";
        _mostFrequentExerciseType = "None";

        LoadStatistics();
    }

    private void LoadStatistics()
    {
        TotalExerciseCount = _statisticsService.GetTotalExerciseCount();
        TotalCaloriesBurned = Math.Round(_statisticsService.GetTotalCaloriesBurned(), 1);
        TotalCaloriesConsumed = Math.Round(_statisticsService.GetTotalCaloriesConsumed(), 1);

        var weekStart = GetStartOfCurrentWeek();
        var weekEnd = weekStart.AddDays(7);
        WeeklyCaloriesBurned = Math.Round(_statisticsService.GetCaloriesBurnedInPeriod(weekStart, weekEnd), 1);
        WeeklyCaloriesConsumed = Math.Round(_statisticsService.GetCaloriesConsumedInPeriod(weekStart, weekEnd), 1);

        var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);
        MonthlyCaloriesBurned = Math.Round(_statisticsService.GetCaloriesBurnedInPeriod(monthStart, monthEnd), 1);
        MonthlyCaloriesConsumed = Math.Round(_statisticsService.GetCaloriesConsumedInPeriod(monthStart, monthEnd), 1);

        var avgDuration = _statisticsService.GetAverageTrainingDuration();
        AverageTrainingDuration = $"{(int)avgDuration.TotalMinutes} min";

        var mostFrequent = _statisticsService.GetMostFrequentExerciseType();
        MostFrequentExerciseType = mostFrequent?.ToString() ?? "None";

        WeeklyBurnedGoalProgress = Math.Round(_statisticsService.GetWeeklyBurnedGoalProgress(), 1);
        WeeklyConsumedGoalProgress = Math.Round(_statisticsService.GetWeeklyConsumedGoalProgress(), 1);
        WeeklyExerciseCountProgress = Math.Round(_statisticsService.GetWeeklyExerciseCountProgress(), 1);
    }

    private static DateTime GetStartOfCurrentWeek()
    {
        var today = DateTime.Today;
        var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        return today.AddDays(-diff);
    }
}
