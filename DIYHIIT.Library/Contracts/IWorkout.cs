using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IWorkout : IEntity
    {
        // Data
        string Name { get; set; }
        string BodyFocus { get; set; }
        string ExerciseCount { get; set; }

        double? RestInterval { get; set; }
        double? ActiveInterval { get; set; }
        double? Effort { get; set; }
        double? Duration { get; set; }

        DateTime? DateAdded { get; set; }
        DateTime? DateUsed { get; set; }

        string ExerciseIDs { get; set; }

        WorkoutType Type { get; set; }

        // View Components
        string BackgroundImage { get; set; }
    }
}
