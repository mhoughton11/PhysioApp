using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IWorkout : IBaseModel
    {
        double? RestInterval { get; set; }
        double? ActiveInterval { get; set; }
        double? Effort { get; set; }
        double? Duration { get; }

        int ExerciseCount { get; }

        DateTime DateAdded { get; set; }
        DateTime DateUsed { get; set; }
        
        List<Exercise> Exercises { get; set; }

        double GetDuration();

        void GetExercises(string value);
    }
}
