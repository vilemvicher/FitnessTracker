using System.Collections.ObjectModel;
using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for adding a new meal entry.
/// </summary>
public class AddMealViewModel : ViewModelBase
{
    private readonly IMealService _mealService;
    private readonly INavigationService _navigationService;

    private string _name;
    private DateTime _selectedDate;
    private string _caloriesText;
    private string _proteinGramsText;
    private string _carbohydratesGramsText;
    private string _fatGramsText;
    private double _servingSize;
    private string _notes;
    private ObservableCollection<MealPreset> _presets;
    private MealPreset? _selectedPreset;
    private string _validationMessage;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public string CaloriesText
    {
        get => _caloriesText;
        set => SetProperty(ref _caloriesText, value);
    }

    public string ProteinGramsText
    {
        get => _proteinGramsText;
        set => SetProperty(ref _proteinGramsText, value);
    }

    public string CarbohydratesGramsText
    {
        get => _carbohydratesGramsText;
        set => SetProperty(ref _carbohydratesGramsText, value);
    }

    public string FatGramsText
    {
        get => _fatGramsText;
        set => SetProperty(ref _fatGramsText, value);
    }

    public double ServingSize
    {
        get => _servingSize;
        set => SetProperty(ref _servingSize, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public ObservableCollection<MealPreset> Presets
    {
        get => _presets;
        set => SetProperty(ref _presets, value);
    }

    public MealPreset? SelectedPreset
    {
        get => _selectedPreset;
        set
        {
            if (SetProperty(ref _selectedPreset, value) && value != null)
            {
                ApplyPreset(value);
            }
        }
    }

    public string ValidationMessage
    {
        get => _validationMessage;
        set => SetProperty(ref _validationMessage, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddMealViewModel(IMealService mealService, INavigationService navigationService)
    {
        _mealService = mealService;
        _navigationService = navigationService;

        _name = string.Empty;
        _selectedDate = DateTime.Now;
        _caloriesText = string.Empty;
        _proteinGramsText = string.Empty;
        _carbohydratesGramsText = string.Empty;
        _fatGramsText = string.Empty;
        _servingSize = 1.0;
        _notes = string.Empty;
        _validationMessage = string.Empty;
        _presets = new ObservableCollection<MealPreset>();

        SaveCommand = new RelayCommand(async _ => await SaveMealAsync());
        CancelCommand = new RelayCommand(_ => _navigationService.NavigateTo<MealListViewModel>());

        _ = LoadPresetsAsync();
    }

    private async Task LoadPresetsAsync()
    {
        var presets = await _mealService.GetPresetsAsync();
        Presets = new ObservableCollection<MealPreset>(presets);
    }

    private void ApplyPreset(MealPreset preset)
    {
        Name = preset.Name;
        CaloriesText = preset.Nutrition.Calories.ToString();
        ProteinGramsText = preset.Nutrition.ProteinGrams.ToString();
        CarbohydratesGramsText = preset.Nutrition.CarbohydratesGrams.ToString();
        FatGramsText = preset.Nutrition.FatGrams.ToString();
        ValidationMessage = string.Empty;
    }

    private bool Validate(out double calories, out double protein, out double carbs, out double fat)
    {
        calories = 0;
        protein = 0;
        carbs = 0;
        fat = 0;

        if (string.IsNullOrWhiteSpace(_name))
        {
            ValidationMessage = "Meal name is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(_caloriesText) || !double.TryParse(_caloriesText, out calories) || calories <= 0)
        {
            ValidationMessage = "Calories must be a positive number.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(_proteinGramsText) || !double.TryParse(_proteinGramsText, out protein) || protein < 0)
        {
            ValidationMessage = "Protein must be a valid number (0 or greater).";
            return false;
        }

        if (string.IsNullOrWhiteSpace(_carbohydratesGramsText) || !double.TryParse(_carbohydratesGramsText, out carbs) || carbs < 0)
        {
            ValidationMessage = "Carbohydrates must be a valid number (0 or greater).";
            return false;
        }

        if (string.IsNullOrWhiteSpace(_fatGramsText) || !double.TryParse(_fatGramsText, out fat) || fat < 0)
        {
            ValidationMessage = "Fat must be a valid number (0 or greater).";
            return false;
        }

        if (_servingSize <= 0)
        {
            ValidationMessage = "Serving size must be greater than 0.";
            return false;
        }

        ValidationMessage = string.Empty;
        return true;
    }

    private async Task SaveMealAsync()
    {
        if (!Validate(out var calories, out var protein, out var carbs, out var fat))
        {
            return;
        }

        var meal = new Meal
        {
            Name = _name,
            Date = _selectedDate,
            Nutrition = new NutritionInfo
            {
                Calories = calories,
                ProteinGrams = protein,
                CarbohydratesGrams = carbs,
                FatGrams = fat
            },
            ServingSize = _servingSize,
            Notes = _notes
        };

        await _mealService.AddMealAsync(meal);
        _navigationService.NavigateTo<MealListViewModel>();
    }
}
