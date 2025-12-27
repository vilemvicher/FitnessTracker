using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.ViewModels;
using FitnessTracker.ViewModels.Base;

namespace FitnessTracker;

/// <summary>
/// Application entry point with dependency injection setup.
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Register services as singletons
        services.AddSingleton<IDataService, JsonDataService>();
        services.AddSingleton<IExerciseService, ExerciseService>();
        services.AddSingleton<IMealService, MealService>();
        services.AddSingleton<IStatisticsService, StatisticsService>();
        services.AddSingleton<CalorieCalculator>();

        // Register navigation service with factory
        services.AddSingleton<INavigationService>(provider =>
        {
            return new NavigationService(type => (ViewModelBase)provider.GetRequiredService(type));
        });

        // Register ViewModels as transient
        services.AddTransient<MainViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<ExerciseListViewModel>();
        services.AddTransient<AddExerciseViewModel>();
        services.AddTransient<MealListViewModel>();
        services.AddTransient<AddMealViewModel>();
        services.AddTransient<StatisticsViewModel>();
        services.AddTransient<UserProfileViewModel>();

        // Register MainWindow
        services.AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Load data at startup
        var dataService = _serviceProvider.GetRequiredService<IDataService>();
        await dataService.LoadDataAsync();

        // Create and show main window with ViewModel
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.Show();
    }
}
