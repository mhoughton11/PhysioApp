using System;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Contracts
{
    public interface IExercise : IEntity
    {
        int? Index { get; set; }

        string Name { get; set; }
        string DisplayName { get; set; }

        string Description { get; set; }
        string ImageURL { get; set; }
        
        double? Duration { get; set; }

        WorkoutType Type { get; set; }
    }
}
