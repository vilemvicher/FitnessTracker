using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces;

/// <summary>
/// Service for meal-related operations.
/// </summary>
public interface IMealService
{
    /// <summary>
    /// Gets all recorded meals.
    /// </summary>
    IReadOnlyList<Meal> GetAllMeals();

    /// <summary>
    /// Gets meals filtered by date range.
    /// </summary>
    IReadOnlyList<Meal> GetFilteredMeals(DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Adds a new meal record.
    /// </summary>
    Task AddMealAsync(Meal meal);

    /// <summary>
    /// Deletes a meal by its ID.
    /// </summary>
    Task DeleteMealAsync(Guid mealId);

    /// <summary>
    /// Gets all available meal presets.
    /// </summary>
    Task<IReadOnlyList<MealPreset>> GetPresetsAsync();
}
