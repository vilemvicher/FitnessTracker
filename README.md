# Fitness Tracker

A WPF desktop application for tracking exercises, meals, and fitness statistics.

## Features

### Exercise Management
- Add and delete exercises with preset selection
- Filter exercises by type, date, and duration
- Auto-calculate calories burned based on user weight
- 13 built-in exercise presets (running, swimming, cycling, etc.)

### Meal Tracking
- Add and delete meals with preset selection
- Track calories and macros (protein, carbs, fat)
- Serving size multiplier for easy portion adjustments
- 10 built-in meal presets

### Statistics Dashboard
- Total/weekly/monthly calories burned
- Total/weekly calories consumed
- Average training duration
- Most frequent exercise type
- Goal progress tracking

### User Profile
- Personal info (name, weight, height, age, gender)
- BMR calculator using Mifflin-St Jeor equation
- Recommended daily calorie intake
- Customizable weekly goals

## Requirements

- Windows 10/11
- .NET 9.0 Runtime

## How to Run

### Option 1: From Source
1. Clone the repository
2. Open `FitnessTracker.sln` in Visual Studio 2022 or Rider
3. Build and run (F5)

### Option 2: Command Line
```bash
cd FitnessTracker
dotnet build
dotnet run
```

## Data Storage

User data is stored in JSON files at:
```
%LocalAppData%\FitnessTracker\
├── exercises.json
├── meals.json
└── profile.json
```

Sample data is created automatically on first run.

## Architecture

- **Pattern**: MVVM (Model-View-ViewModel)
- **DI**: Microsoft.Extensions.DependencyInjection
- **Storage**: JSON files (System.Text.Json)

### Project Structure
```
FitnessTracker/
├── Models/           # Domain entities
├── ViewModels/       # UI logic and data binding
├── Views/            # XAML UI pages
├── Services/         # Business logic and data access
├── Converters/       # WPF value converters
└── Resources/        # Default data and presets
```

## Tech Stack

- .NET 9.0
- WPF (Windows Presentation Foundation)
- System.Text.Json
- Microsoft.Extensions.DependencyInjection
