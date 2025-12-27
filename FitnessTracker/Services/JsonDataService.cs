using System.IO;
using System.Text.Json;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;

namespace FitnessTracker.Services;

/// <summary>
/// JSON file-based implementation of IDataService.
/// Uses separate files for exercises, meals, profile, and presets.
/// </summary>
public class JsonDataService : IDataService
{
    private readonly string _appDataPath;
    private readonly string _exercisesFilePath;
    private readonly string _mealsFilePath;
    private readonly string _profileFilePath;
    private readonly string _presetsFilePath;
    private readonly JsonSerializerOptions _jsonOptions;
    private FitnessData _data;
    private PresetData? _cachedPresets;

    public event EventHandler? DataChanged;

    public FitnessData Data => _data;

    public JsonDataService()
    {
        _appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FitnessTracker");

        Directory.CreateDirectory(_appDataPath);

        _exercisesFilePath = Path.Combine(_appDataPath, "exercises.json");
        _mealsFilePath = Path.Combine(_appDataPath, "meals.json");
        _profileFilePath = Path.Combine(_appDataPath, "profile.json");
        _presetsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DefaultData.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        _data = new FitnessData();
    }

    public async Task LoadDataAsync()
    {
        _data = new FitnessData();

        // Load exercises
        _data.Exercises = await LoadListFromFileAsync<Exercise>(_exercisesFilePath);

        // Load meals
        _data.Meals = await LoadListFromFileAsync<Meal>(_mealsFilePath);

        // Load profile
        _data.UserProfile = await LoadFromFileAsync<UserProfile>(_profileFilePath) ?? new UserProfile();

        // If no data exists, create default sample data
        if (_data.Exercises.Count == 0 && _data.Meals.Count == 0)
        {
            await CreateDefaultSampleDataAsync();
        }
    }

    public async Task SaveDataAsync()
    {
        _data.LastModified = DateTime.Now;

        // Save exercises to separate file
        await SaveToFileAsync(_exercisesFilePath, _data.Exercises);

        // Save meals to separate file
        await SaveToFileAsync(_mealsFilePath, _data.Meals);

        // Save profile to separate file
        await SaveToFileAsync(_profileFilePath, _data.UserProfile);

        DataChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task<PresetData> LoadPresetsAsync()
    {
        if (_cachedPresets != null)
            return _cachedPresets;

        if (!File.Exists(_presetsFilePath))
        {
            _cachedPresets = CreateDefaultPresets();
            return _cachedPresets;
        }

        try
        {
            var json = await File.ReadAllTextAsync(_presetsFilePath);
            _cachedPresets = JsonSerializer.Deserialize<PresetData>(json, _jsonOptions) ?? CreateDefaultPresets();
        }
        catch (JsonException)
        {
            _cachedPresets = CreateDefaultPresets();
        }

        return _cachedPresets;
    }

    private async Task<List<T>> LoadListFromFileAsync<T>(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<T>();

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
        }
        catch (JsonException)
        {
            return new List<T>();
        }
    }

    private async Task<T?> LoadFromFileAsync<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
            return null;

        try
        {
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private async Task SaveToFileAsync<T>(string filePath, T data)
    {
        var json = JsonSerializer.Serialize(data, _jsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    /// <summary>
    /// Creates default sample exercises and meals for new users.
    /// </summary>
    private async Task CreateDefaultSampleDataAsync()
    {
        // Add sample exercises
        _data.Exercises = new List<Exercise>
        {
            new Exercise
            {
                Type = ExerciseType.Running,
                Date = DateTime.Now.AddDays(-1),
                Duration = TimeSpan.FromMinutes(30),
                CaloriesBurned = 300,
                Notes = "Morning jog in the park"
            },
            new Exercise
            {
                Type = ExerciseType.Weightlifting,
                Date = DateTime.Now.AddDays(-2),
                Duration = TimeSpan.FromMinutes(45),
                CaloriesBurned = 225,
                Notes = "Upper body workout"
            },
            new Exercise
            {
                Type = ExerciseType.Cycling,
                Date = DateTime.Now.AddDays(-3),
                Duration = TimeSpan.FromMinutes(60),
                CaloriesBurned = 540,
                Notes = "Evening bike ride"
            },
            new Exercise
            {
                Type = ExerciseType.Swimming,
                Date = DateTime.Now.AddDays(-5),
                Duration = TimeSpan.FromMinutes(40),
                CaloriesBurned = 320,
                Notes = "Freestyle swimming"
            },
            new Exercise
            {
                Type = ExerciseType.Yoga,
                Date = DateTime.Now.AddDays(-6),
                Duration = TimeSpan.FromMinutes(50),
                CaloriesBurned = 150,
                Notes = "Relaxing yoga session"
            }
        };

        // Add sample meals
        _data.Meals = new List<Meal>
        {
            new Meal
            {
                Name = "Oatmeal with Berries",
                Date = DateTime.Now.AddDays(-1).Date.AddHours(8),
                Nutrition = new NutritionInfo { Calories = 350, ProteinGrams = 12, CarbohydratesGrams = 55, FatGrams = 8 },
                ServingSize = 1.0,
                Notes = "Breakfast"
            },
            new Meal
            {
                Name = "Grilled Chicken Salad",
                Date = DateTime.Now.AddDays(-1).Date.AddHours(13),
                Nutrition = new NutritionInfo { Calories = 450, ProteinGrams = 42, CarbohydratesGrams = 20, FatGrams = 22 },
                ServingSize = 1.0,
                Notes = "Lunch"
            },
            new Meal
            {
                Name = "Salmon with Brown Rice",
                Date = DateTime.Now.AddDays(-1).Date.AddHours(19),
                Nutrition = new NutritionInfo { Calories = 570, ProteinGrams = 45, CarbohydratesGrams = 45, FatGrams = 22 },
                ServingSize = 1.0,
                Notes = "Dinner"
            },
            new Meal
            {
                Name = "Protein Shake",
                Date = DateTime.Now.AddDays(-2).Date.AddHours(10),
                Nutrition = new NutritionInfo { Calories = 180, ProteinGrams = 30, CarbohydratesGrams = 8, FatGrams = 3 },
                ServingSize = 1.0,
                Notes = "Post-workout snack"
            },
            new Meal
            {
                Name = "Greek Yogurt with Almonds",
                Date = DateTime.Now.AddDays(-2).Date.AddHours(15),
                Nutrition = new NutritionInfo { Calories = 280, ProteinGrams = 18, CarbohydratesGrams = 20, FatGrams = 14 },
                ServingSize = 1.0,
                Notes = "Afternoon snack"
            }
        };

        // Save the default data
        await SaveDataAsync();
    }

    /// <summary>
    /// Creates default presets if the file doesn't exist.
    /// </summary>
    private PresetData CreateDefaultPresets()
    {
        return new PresetData
        {
            ExercisePresets = new List<ExercisePreset>
            {
                new() { Type = ExerciseType.Running, Name = "Running (moderate)", CaloriesPerMinute = 10, Description = "Jogging at 8-10 km/h" },
                new() { Type = ExerciseType.Running, Name = "Running (fast)", CaloriesPerMinute = 14, Description = "Running at 12+ km/h" },
                new() { Type = ExerciseType.Swimming, Name = "Swimming (freestyle)", CaloriesPerMinute = 8, Description = "Moderate freestyle" },
                new() { Type = ExerciseType.Weightlifting, Name = "Weight Training", CaloriesPerMinute = 5, Description = "General weight training" },
                new() { Type = ExerciseType.Cycling, Name = "Cycling (moderate)", CaloriesPerMinute = 9, Description = "Cycling at 20-25 km/h" },
                new() { Type = ExerciseType.Walking, Name = "Walking (brisk)", CaloriesPerMinute = 4.5, Description = "Brisk walking" },
                new() { Type = ExerciseType.Yoga, Name = "Yoga", CaloriesPerMinute = 3, Description = "Hatha yoga" },
                new() { Type = ExerciseType.HIIT, Name = "HIIT Workout", CaloriesPerMinute = 12, Description = "High-intensity interval" }
            },
            MealPresets = new List<MealPreset>
            {
                new() { Name = "Oatmeal with Berries", Category = "Breakfast", Nutrition = new() { Calories = 350, ProteinGrams = 12, CarbohydratesGrams = 55, FatGrams = 8 } },
                new() { Name = "Scrambled Eggs", Category = "Breakfast", Nutrition = new() { Calories = 200, ProteinGrams = 14, CarbohydratesGrams = 2, FatGrams = 15 } },
                new() { Name = "Grilled Chicken Breast", Category = "Lunch", Nutrition = new() { Calories = 280, ProteinGrams = 53, CarbohydratesGrams = 0, FatGrams = 6 } },
                new() { Name = "Caesar Salad", Category = "Lunch", Nutrition = new() { Calories = 320, ProteinGrams = 8, CarbohydratesGrams = 12, FatGrams = 28 } },
                new() { Name = "Salmon Fillet", Category = "Dinner", Nutrition = new() { Calories = 350, ProteinGrams = 40, CarbohydratesGrams = 0, FatGrams = 20 } },
                new() { Name = "Brown Rice", Category = "Dinner", Nutrition = new() { Calories = 220, ProteinGrams = 5, CarbohydratesGrams = 45, FatGrams = 2 } },
                new() { Name = "Protein Shake", Category = "Snack", Nutrition = new() { Calories = 180, ProteinGrams = 30, CarbohydratesGrams = 8, FatGrams = 3 } },
                new() { Name = "Apple", Category = "Snack", Nutrition = new() { Calories = 95, ProteinGrams = 0.5, CarbohydratesGrams = 25, FatGrams = 0.3 } }
            }
        };
    }
}
