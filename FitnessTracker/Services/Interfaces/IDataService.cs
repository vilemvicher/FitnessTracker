using FitnessTracker.Models;

namespace FitnessTracker.Services.Interfaces;

/// <summary>
/// Abstraction for data persistence operations.
/// </summary>
public interface IDataService
{
    /// <summary>
    /// Gets the current fitness data from memory.
    /// </summary>
    FitnessData Data { get; }

    /// <summary>
    /// Loads fitness data from storage into memory.
    /// </summary>
    Task LoadDataAsync();

    /// <summary>
    /// Saves current fitness data to storage.
    /// </summary>
    Task SaveDataAsync();

    /// <summary>
    /// Loads default preset data (exercises and meals).
    /// </summary>
    Task<PresetData> LoadPresetsAsync();

    /// <summary>
    /// Event raised when data is modified.
    /// </summary>
    event EventHandler? DataChanged;
}
