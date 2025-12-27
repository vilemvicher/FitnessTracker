using System.Collections.ObjectModel;
using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for adding a new exercise entry.
/// </summary>
public class AddExerciseViewModel : ViewModelBase
{
    private readonly IExerciseService _exerciseService;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    private readonly CalorieCalculator _calorieCalculator;

    private ExerciseType _selectedType;
    private DateTime _selectedDate;
    private int _durationMinutes;
    private double _caloriesBurned;
    private string _notes;
    private ObservableCollection<ExercisePreset> _presets;
    private ExercisePreset? _selectedPreset;
    private bool _useAutoCalorieCalculation;

    public ExerciseType SelectedType
    {
        get => _selectedType;
        set => SetProperty(ref _selectedType, value);
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            if (SetProperty(ref _durationMinutes, value))
                CalculateCaloriesIfAuto();
        }
    }

    public double CaloriesBurned
    {
        get => _caloriesBurned;
        set => SetProperty(ref _caloriesBurned, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public ObservableCollection<ExercisePreset> Presets
    {
        get => _presets;
        set => SetProperty(ref _presets, value);
    }

    public ExercisePreset? SelectedPreset
    {
        get => _selectedPreset;
        set
        {
            if (SetProperty(ref _selectedPreset, value) && value != null)
            {
                SelectedType = value.Type;
                CalculateCaloriesIfAuto();
            }
        }
    }

    public bool UseAutoCalorieCalculation
    {
        get => _useAutoCalorieCalculation;
        set
        {
            if (SetProperty(ref _useAutoCalorieCalculation, value))
                CalculateCaloriesIfAuto();
        }
    }

    public IEnumerable<ExerciseType> ExerciseTypes => Enum.GetValues<ExerciseType>();

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddExerciseViewModel(
        IExerciseService exerciseService,
        INavigationService navigationService,
        IDataService dataService,
        CalorieCalculator calorieCalculator)
    {
        _exerciseService = exerciseService;
        _navigationService = navigationService;
        _dataService = dataService;
        _calorieCalculator = calorieCalculator;

        _selectedDate = DateTime.Now;
        _durationMinutes = 30;
        _notes = string.Empty;
        _presets = new ObservableCollection<ExercisePreset>();
        _useAutoCalorieCalculation = true;

        SaveCommand = new RelayCommand(async _ => await SaveExerciseAsync(), _ => CanSave());
        CancelCommand = new RelayCommand(_ => _navigationService.NavigateTo<ExerciseListViewModel>());

        _ = LoadPresetsAsync();
    }

    private async Task LoadPresetsAsync()
    {
        var presets = await _exerciseService.GetPresetsAsync();
        Presets = new ObservableCollection<ExercisePreset>(presets);

        if (Presets.Count > 0)
            SelectedPreset = Presets[0];
    }

    private void CalculateCaloriesIfAuto()
    {
        if (!_useAutoCalorieCalculation || _selectedPreset == null)
            return;

        var userWeight = _dataService.Data.UserProfile.WeightKg;
        CaloriesBurned = Math.Round(
            _calorieCalculator.CalculateCaloriesBurned(_selectedPreset, _durationMinutes, userWeight),
            1);
    }

    private bool CanSave()
    {
        return _durationMinutes > 0 && _caloriesBurned >= 0;
    }

    private async Task SaveExerciseAsync()
    {
        var exercise = new Exercise
        {
            Type = _selectedType,
            Date = _selectedDate,
            Duration = TimeSpan.FromMinutes(_durationMinutes),
            CaloriesBurned = _caloriesBurned,
            Notes = _notes
        };

        await _exerciseService.AddExerciseAsync(exercise);
        _navigationService.NavigateTo<ExerciseListViewModel>();
    }
}
