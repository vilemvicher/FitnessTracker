using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for the dashboard overview page.
/// </summary>
public class DashboardViewModel : ViewModelBase
{
    private readonly IStatisticsService _statisticsService;
    private readonly IExerciseService _exerciseService;
    private readonly IMealService _mealService;

    private int _totalExercises;
    private double _weeklyCaloriesBurned;
    private double _weeklyCaloriesConsumed;
    private double _burnedGoalProgress;
    private double _consumedGoalProgress;
    private double _exerciseGoalProgress;
    private string _mostFrequentExercise;

    public int TotalExercises
    {
        get => _totalExercises;
        set => SetProperty(ref _totalExercises, value);
    }

    public double WeeklyCaloriesBurned
    {
        get => _weeklyCaloriesBurned;
        set => SetProperty(ref _weeklyCaloriesBurned, value);
    }

    public double WeeklyCaloriesConsumed
    {
        get => _weeklyCaloriesConsumed;
        set => SetProperty(ref _weeklyCaloriesConsumed, value);
    }

    public double BurnedGoalProgress
    {
        get => _burnedGoalProgress;
        set => SetProperty(ref _burnedGoalProgress, value);
    }

    public double ConsumedGoalProgress
    {
        get => _consumedGoalProgress;
        set => SetProperty(ref _consumedGoalProgress, value);
    }

    public double ExerciseGoalProgress
    {
        get => _exerciseGoalProgress;
        set => SetProperty(ref _exerciseGoalProgress, value);
    }

    public string MostFrequentExercise
    {
        get => _mostFrequentExercise;
        set => SetProperty(ref _mostFrequentExercise, value);
    }

    public DashboardViewModel(
        IStatisticsService statisticsService,
        IExerciseService exerciseService,
        IMealService mealService)
    {
        _statisticsService = statisticsService;
        _exerciseService = exerciseService;
        _mealService = mealService;
        _mostFrequentExercise = "None";

        LoadStatistics();
    }

    private void LoadStatistics()
    {
        TotalExercises = _statisticsService.GetTotalExerciseCount();

        var weekStart = GetStartOfCurrentWeek();
        var weekEnd = weekStart.AddDays(7);

        WeeklyCaloriesBurned = _statisticsService.GetCaloriesBurnedInPeriod(weekStart, weekEnd);
        WeeklyCaloriesConsumed = _statisticsService.GetCaloriesConsumedInPeriod(weekStart, weekEnd);

        BurnedGoalProgress = _statisticsService.GetWeeklyBurnedGoalProgress();
        ConsumedGoalProgress = _statisticsService.GetWeeklyConsumedGoalProgress();
        ExerciseGoalProgress = _statisticsService.GetWeeklyExerciseCountProgress();

        var mostFrequent = _statisticsService.GetMostFrequentExerciseType();
        MostFrequentExercise = mostFrequent?.ToString() ?? "None";
    }

    private static DateTime GetStartOfCurrentWeek()
    {
        var today = DateTime.Today;
        var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        return today.AddDays(-diff);
    }
}
