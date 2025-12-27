using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.Services;

/// <summary>
/// Service for managing navigation between ViewModels.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly Func<Type, ViewModelBase> _viewModelFactory;
    private ViewModelBase? _currentViewModel;

    public event EventHandler<ViewModelBase>? CurrentViewModelChanged;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            if (value != null)
                CurrentViewModelChanged?.Invoke(this, value);
        }
    }

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        var viewModel = _viewModelFactory(typeof(TViewModel));
        CurrentViewModel = viewModel;
    }
}
