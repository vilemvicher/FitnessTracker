using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for managing user profile and goals.
/// </summary>
public class UserProfileViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private readonly CalorieCalculator _calorieCalculator;

    private string _name;
    private double _weightKg;
    private double _heightCm;
    private int _age;
    private Gender _gender;
    private double _caloriesBurnedTarget;
    private double _caloriesConsumedTarget;
    private int _exerciseCountTarget;
    private bool _hasUnsavedChanges;
    private double _bmr;
    private double _recommendedDailyCalories;

    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value))
                HasUnsavedChanges = true;
        }
    }

    public double WeightKg
    {
        get => _weightKg;
        set
        {
            if (SetProperty(ref _weightKg, value))
            {
                HasUnsavedChanges = true;
                UpdateBMRCalculations();
            }
        }
    }

    public double HeightCm
    {
        get => _heightCm;
        set
        {
            if (SetProperty(ref _heightCm, value))
            {
                HasUnsavedChanges = true;
                UpdateBMRCalculations();
            }
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (SetProperty(ref _age, value))
            {
                HasUnsavedChanges = true;
                UpdateBMRCalculations();
            }
        }
    }

    public Gender Gender
    {
        get => _gender;
        set
        {
            if (SetProperty(ref _gender, value))
            {
                HasUnsavedChanges = true;
                UpdateBMRCalculations();
            }
        }
    }

    public IEnumerable<Gender> Genders => Enum.GetValues<Gender>();

    public double CaloriesBurnedTarget
    {
        get => _caloriesBurnedTarget;
        set
        {
            if (SetProperty(ref _caloriesBurnedTarget, value))
                HasUnsavedChanges = true;
        }
    }

    public double CaloriesConsumedTarget
    {
        get => _caloriesConsumedTarget;
        set
        {
            if (SetProperty(ref _caloriesConsumedTarget, value))
                HasUnsavedChanges = true;
        }
    }

    public int ExerciseCountTarget
    {
        get => _exerciseCountTarget;
        set
        {
            if (SetProperty(ref _exerciseCountTarget, value))
                HasUnsavedChanges = true;
        }
    }

    public bool HasUnsavedChanges
    {
        get => _hasUnsavedChanges;
        set => SetProperty(ref _hasUnsavedChanges, value);
    }

    public double BMR
    {
        get => _bmr;
        set => SetProperty(ref _bmr, value);
    }

    public double RecommendedDailyCalories
    {
        get => _recommendedDailyCalories;
        set => SetProperty(ref _recommendedDailyCalories, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand ResetCommand { get; }

    public UserProfileViewModel(IDataService dataService, CalorieCalculator calorieCalculator)
    {
        _dataService = dataService;
        _calorieCalculator = calorieCalculator;

        _name = string.Empty;

        SaveCommand = new RelayCommand(async _ => await SaveProfileAsync());
        ResetCommand = new RelayCommand(_ => LoadProfile());

        LoadProfile();
    }

    private void LoadProfile()
    {
        var profile = _dataService.Data.UserProfile;
        _name = profile.Name;
        _weightKg = profile.WeightKg;
        _heightCm = profile.HeightCm;
        _age = profile.Age;
        _gender = profile.Gender;
        _caloriesBurnedTarget = profile.Goals.CaloriesBurnedTarget;
        _caloriesConsumedTarget = profile.Goals.CaloriesConsumedTarget;
        _exerciseCountTarget = profile.Goals.ExerciseCountTarget;

        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(WeightKg));
        OnPropertyChanged(nameof(HeightCm));
        OnPropertyChanged(nameof(Age));
        OnPropertyChanged(nameof(Gender));
        OnPropertyChanged(nameof(CaloriesBurnedTarget));
        OnPropertyChanged(nameof(CaloriesConsumedTarget));
        OnPropertyChanged(nameof(ExerciseCountTarget));

        UpdateBMRCalculations();
        HasUnsavedChanges = false;
    }

    private void UpdateBMRCalculations()
    {
        if (_weightKg > 0 && _heightCm > 0 && _age > 0)
        {
            BMR = Math.Round(_calorieCalculator.CalculateBMR(_weightKg, _heightCm, _age, _gender), 0);
            RecommendedDailyCalories = Math.Round(_calorieCalculator.CalculateTDEE(BMR, 1.55), 0);
        }
    }

    private async Task SaveProfileAsync()
    {
        var profile = _dataService.Data.UserProfile;
        profile.Name = _name;
        profile.WeightKg = _weightKg;
        profile.HeightCm = _heightCm;
        profile.Age = _age;
        profile.Gender = _gender;
        profile.Goals.CaloriesBurnedTarget = _caloriesBurnedTarget;
        profile.Goals.CaloriesConsumedTarget = _caloriesConsumedTarget;
        profile.Goals.ExerciseCountTarget = _exerciseCountTarget;

        await _dataService.SaveDataAsync();
        HasUnsavedChanges = false;
    }
}
