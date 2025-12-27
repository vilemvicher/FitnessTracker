using System.Collections.ObjectModel;
using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for displaying meal history.
/// </summary>
public class MealListViewModel : ViewModelBase
{
    private readonly IMealService _mealService;
    private readonly INavigationService _navigationService;

    private ObservableCollection<Meal> _meals;
    private DateTime? _startDateFilter;
    private DateTime? _endDateFilter;
    private Meal? _selectedMeal;

    public ObservableCollection<Meal> Meals
    {
        get => _meals;
        set => SetProperty(ref _meals, value);
    }

    public DateTime? StartDateFilter
    {
        get => _startDateFilter;
        set
        {
            if (SetProperty(ref _startDateFilter, value))
                ApplyFilters();
        }
    }

    public DateTime? EndDateFilter
    {
        get => _endDateFilter;
        set
        {
            if (SetProperty(ref _endDateFilter, value))
                ApplyFilters();
        }
    }

    public Meal? SelectedMeal
    {
        get => _selectedMeal;
        set => SetProperty(ref _selectedMeal, value);
    }

    public ICommand DeleteMealCommand { get; }
    public ICommand ClearFiltersCommand { get; }
    public ICommand AddMealCommand { get; }

    public MealListViewModel(IMealService mealService, INavigationService navigationService)
    {
        _mealService = mealService;
        _navigationService = navigationService;
        _meals = new ObservableCollection<Meal>();

        DeleteMealCommand = new RelayCommand(async _ => await DeleteSelectedMealAsync(), _ => SelectedMeal != null);
        ClearFiltersCommand = new RelayCommand(_ => ClearFilters());
        AddMealCommand = new RelayCommand(_ => _navigationService.NavigateTo<AddMealViewModel>());

        LoadMeals();
    }

    private void LoadMeals()
    {
        var meals = _mealService.GetAllMeals();
        Meals = new ObservableCollection<Meal>(meals);
    }

    private void ApplyFilters()
    {
        var filtered = _mealService.GetFilteredMeals(_startDateFilter, _endDateFilter);
        Meals = new ObservableCollection<Meal>(filtered);
    }

    private void ClearFilters()
    {
        _startDateFilter = null;
        _endDateFilter = null;
        OnPropertyChanged(nameof(StartDateFilter));
        OnPropertyChanged(nameof(EndDateFilter));
        LoadMeals();
    }

    private async Task DeleteSelectedMealAsync()
    {
        if (SelectedMeal == null)
            return;

        await _mealService.DeleteMealAsync(SelectedMeal.Id);
        Meals.Remove(SelectedMeal);
        SelectedMeal = null;
    }
}
