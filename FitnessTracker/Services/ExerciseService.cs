using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services;

/// <summary>
/// Service for managing exercise records.
/// </summary>
public class ExerciseService : IExerciseService
{
    private readonly IDataService _dataService;

    public ExerciseService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public IReadOnlyList<Exercise> GetAllExercises()
    {
        return _dataService.Data.Exercises.OrderByDescending(e => e.Date).ToList();
    }

    public IReadOnlyList<Exercise> GetFilteredExercises(
        ExerciseType? type = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        TimeSpan? minDuration = null)
    {
        var query = _dataService.Data.Exercises.AsEnumerable();

        if (type.HasValue)
            query = query.Where(e => e.Type == type.Value);

        if (startDate.HasValue)
            query = query.Where(e => e.Date.Date >= startDate.Value.Date);

        if (endDate.HasValue)
            query = query.Where(e => e.Date.Date <= endDate.Value.Date);

        if (minDuration.HasValue)
            query = query.Where(e => e.Duration >= minDuration.Value);

        return query.OrderByDescending(e => e.Date).ToList();
    }

    public async Task AddExerciseAsync(Exercise exercise)
    {
        _dataService.Data.Exercises.Add(exercise);
        await _dataService.SaveDataAsync();
    }

    public async Task DeleteExerciseAsync(Guid exerciseId)
    {
        var exercise = _dataService.Data.Exercises.FirstOrDefault(e => e.Id == exerciseId);
        if (exercise != null)
        {
            _dataService.Data.Exercises.Remove(exercise);
            await _dataService.SaveDataAsync();
        }
    }

    public async Task<IReadOnlyList<ExercisePreset>> GetPresetsAsync()
    {
        var presets = await _dataService.LoadPresetsAsync();
        return presets.ExercisePresets;
    }
}
