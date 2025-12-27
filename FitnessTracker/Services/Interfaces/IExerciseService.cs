using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces;

/// <summary>
/// Service for exercise-related operations.
/// </summary>
public interface IExerciseService
{
    /// <summary>
    /// Gets all recorded exercises.
    /// </summary>
    IReadOnlyList<Exercise> GetAllExercises();

    /// <summary>
    /// Gets exercises filtered by specified criteria.
    /// </summary>
    IReadOnlyList<Exercise> GetFilteredExercises(
        ExerciseType? type = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        TimeSpan? minDuration = null);

    /// <summary>
    /// Adds a new exercise record.
    /// </summary>
    Task AddExerciseAsync(Exercise exercise);

    /// <summary>
    /// Deletes an exercise by its ID.
    /// </summary>
    Task DeleteExerciseAsync(Guid exerciseId);

    /// <summary>
    /// Gets all available exercise presets.
    /// </summary>
    Task<IReadOnlyList<ExercisePreset>> GetPresetsAsync();
}
