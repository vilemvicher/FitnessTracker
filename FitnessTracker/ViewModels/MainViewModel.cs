using System.Windows.Input;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// Shell ViewModel managing navigation and application-wide state.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private ViewModelBase? _currentViewModel;
    private string _currentPageTitle;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    public string CurrentPageTitle
    {
        get => _currentPageTitle;
        set => SetProperty(ref _currentPageTitle, value);
    }

    public ICommand NavigateToDashboardCommand { get; }
    public ICommand NavigateToExercisesCommand { get; }
    public ICommand NavigateToAddExerciseCommand { get; }
    public ICommand NavigateToMealsCommand { get; }
    public ICommand NavigateToAddMealCommand { get; }
    public ICommand NavigateToStatisticsCommand { get; }
    public ICommand NavigateToProfileCommand { get; }

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _currentPageTitle = "Dashboard";

        _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

        NavigateToDashboardCommand = new RelayCommand(_ => NavigateToDashboard());
        NavigateToExercisesCommand = new RelayCommand(_ => NavigateToExercises());
        NavigateToAddExerciseCommand = new RelayCommand(_ => NavigateToAddExercise());
        NavigateToMealsCommand = new RelayCommand(_ => NavigateToMeals());
        NavigateToAddMealCommand = new RelayCommand(_ => NavigateToAddMeal());
        NavigateToStatisticsCommand = new RelayCommand(_ => NavigateToStatistics());
        NavigateToProfileCommand = new RelayCommand(_ => NavigateToProfile());

        NavigateToDashboard();
    }

    private void OnCurrentViewModelChanged(object? sender, ViewModelBase viewModel)
    {
        CurrentViewModel = viewModel;
    }

    private void NavigateToDashboard()
    {
        CurrentPageTitle = "Dashboard";
        _navigationService.NavigateTo<DashboardViewModel>();
    }

    private void NavigateToExercises()
    {
        CurrentPageTitle = "Exercises";
        _navigationService.NavigateTo<ExerciseListViewModel>();
    }

    private void NavigateToAddExercise()
    {
        CurrentPageTitle = "Add Exercise";
        _navigationService.NavigateTo<AddExerciseViewModel>();
    }

    private void NavigateToMeals()
    {
        CurrentPageTitle = "Meals";
        _navigationService.NavigateTo<MealListViewModel>();
    }

    private void NavigateToAddMeal()
    {
        CurrentPageTitle = "Add Meal";
        _navigationService.NavigateTo<AddMealViewModel>();
    }

    private void NavigateToStatistics()
    {
        CurrentPageTitle = "Statistics";
        _navigationService.NavigateTo<StatisticsViewModel>();
    }

    private void NavigateToProfile()
    {
        CurrentPageTitle = "Profile";
        _navigationService.NavigateTo<UserProfileViewModel>();
    }
}
