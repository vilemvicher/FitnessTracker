using System.Collections.ObjectModel;
using System.Windows.Input;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.ViewModels;

/// <summary>
/// ViewModel for displaying and filtering exercise history.
/// </summary>
public class ExerciseListViewModel : ViewModelBase
{
    private readonly IExerciseService _exerciseService;
    private readonly INavigationService _navigationService;

    private ObservableCollection<Exercise> _exercises;
    private ExerciseType? _selectedTypeFilter;
    private DateTime? _startDateFilter;
    private DateTime? _endDateFilter;
    private Exercise? _selectedExercise;

    public ObservableCollection<Exercise> Exercises
    {
        get => _exercises;
        set => SetProperty(ref _exercises, value);
    }

    public ExerciseType? SelectedTypeFilter
    {
        get => _selectedTypeFilter;
        set
        {
            if (SetProperty(ref _selectedTypeFilter, value))
                ApplyFilters();
        }
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

    public Exercise? SelectedExercise
    {
        get => _selectedExercise;
        set => SetProperty(ref _selectedExercise, value);
    }

    public IEnumerable<ExerciseType> ExerciseTypes => Enum.GetValues<ExerciseType>();

    public ICommand DeleteExerciseCommand { get; }
    public ICommand ClearFiltersCommand { get; }
    public ICommand AddExerciseCommand { get; }

    public ExerciseListViewModel(IExerciseService exerciseService, INavigationService navigationService)
    {
        _exerciseService = exerciseService;
        _navigationService = navigationService;
        _exercises = new ObservableCollection<Exercise>();

        DeleteExerciseCommand = new RelayCommand(async _ => await DeleteSelectedExerciseAsync(), _ => SelectedExercise != null);
        ClearFiltersCommand = new RelayCommand(_ => ClearFilters());
        AddExerciseCommand = new RelayCommand(_ => _navigationService.NavigateTo<AddExerciseViewModel>());

        LoadExercises();
    }

    private void LoadExercises()
    {
        var exercises = _exerciseService.GetAllExercises();
        Exercises = new ObservableCollection<Exercise>(exercises);
    }

    private void ApplyFilters()
    {
        var filtered = _exerciseService.GetFilteredExercises(
            _selectedTypeFilter,
            _startDateFilter,
            _endDateFilter);

        Exercises = new ObservableCollection<Exercise>(filtered);
    }

    private void ClearFilters()
    {
        _selectedTypeFilter = null;
        _startDateFilter = null;
        _endDateFilter = null;
        OnPropertyChanged(nameof(SelectedTypeFilter));
        OnPropertyChanged(nameof(StartDateFilter));
        OnPropertyChanged(nameof(EndDateFilter));
        LoadExercises();
    }

    private async Task DeleteSelectedExerciseAsync()
    {
        if (SelectedExercise == null)
            return;

        await _exerciseService.DeleteExerciseAsync(SelectedExercise.Id);
        Exercises.Remove(SelectedExercise);
        SelectedExercise = null;
    }
}
