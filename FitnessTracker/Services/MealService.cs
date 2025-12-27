using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services;

/// <summary>
/// Service for managing meal records.
/// </summary>
public class MealService : IMealService
{
    private readonly IDataService _dataService;

    public MealService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public IReadOnlyList<Meal> GetAllMeals()
    {
        return _dataService.Data.Meals.OrderByDescending(m => m.Date).ToList();
    }

    public IReadOnlyList<Meal> GetFilteredMeals(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _dataService.Data.Meals.AsEnumerable();

        if (startDate.HasValue)
            query = query.Where(m => m.Date.Date >= startDate.Value.Date);

        if (endDate.HasValue)
            query = query.Where(m => m.Date.Date <= endDate.Value.Date);

        return query.OrderByDescending(m => m.Date).ToList();
    }

    public async Task AddMealAsync(Meal meal)
    {
        _dataService.Data.Meals.Add(meal);
        await _dataService.SaveDataAsync();
    }

    public async Task DeleteMealAsync(Guid mealId)
    {
        var meal = _dataService.Data.Meals.FirstOrDefault(m => m.Id == mealId);
        if (meal != null)
        {
            _dataService.Data.Meals.Remove(meal);
            await _dataService.SaveDataAsync();
        }
    }

    public async Task<IReadOnlyList<MealPreset>> GetPresetsAsync()
    {
        var presets = await _dataService.LoadPresetsAsync();
        return presets.MealPresets;
    }
}
