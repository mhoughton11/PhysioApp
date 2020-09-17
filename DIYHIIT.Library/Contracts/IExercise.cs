using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IExercise : IEntity
    {
        int? Index { get; set; }

        string Name { get; set; }
        string DisplayName { get; set; }

        string Description { get; set; }
        string BodyFocus { get; set; }
        string ImageURL { get; set; }
        
        double? Duration { get; set; }

        WorkoutType Type { get; set; }

        int WorkoutId { get; set; }
    }
}
