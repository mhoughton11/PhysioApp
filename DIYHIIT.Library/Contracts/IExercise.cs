using System;
using DIYHIIT.Library.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Library.Contracts
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
