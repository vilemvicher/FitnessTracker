namespace FitnessTracker.Models;

/// <summary>
/// Container for default preset data loaded from JSON.
/// </summary>
public class PresetData
{
    private List<ExercisePreset> _exercisePresets;
    private List<MealPreset> _mealPresets;

    /// <summary>
    /// Collection of default exercise presets.
    /// </summary>
    public List<ExercisePreset> ExercisePresets
    {
        get => _exercisePresets;
        set => _exercisePresets = value;
    }

    /// <summary>
    /// Collection of default meal presets.
    /// </summary>
    public List<MealPreset> MealPresets
    {
        get => _mealPresets;
        set => _mealPresets = value;
    }

    public PresetData()
    {
        _exercisePresets = new List<ExercisePreset>();
        _mealPresets = new List<MealPreset>();
    }
}
