using FitnessTracker.ViewModels.Base;

namespace FitnessTracker.Services.Interfaces;

/// <summary>
/// Abstraction for navigation between views.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Gets the currently displayed ViewModel.
    /// </summary>
    ViewModelBase? CurrentViewModel { get; }

    /// <summary>
    /// Navigates to the specified ViewModel type.
    /// </summary>
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

    /// <summary>
    /// Event raised when navigation occurs.
    /// </summary>
    event EventHandler<ViewModelBase>? CurrentViewModelChanged;
}
