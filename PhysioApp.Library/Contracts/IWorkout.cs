﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Contracts
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

        string ExerciseIds { get; set; }

        WorkoutType Type { get; set; }

        // View Components
        string BackgroundImage { get; set; }
    }
}
