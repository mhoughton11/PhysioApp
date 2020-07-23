using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIYHIIT.Models.Workout
{
    public interface IWorkout : IBaseModel
    {
        double RestInterval { get; set; }
        double ActiveInterval { get; set; }
        string ExercisesString { get; set; }
        string CellBackgroundColour { get; }
        int ExerciseCount { get; }
        DateTime DateAdded { get; set; }
        DateTime DateUsed { get; set; }
        double Effort { get; set; }
        double Duration { get; }
        List<Exercise.Exercise> Exercises { get; set; }

        double GetDuration();

        string GetBackgroundColour(string workoutType);

        Task GetExercises(string value);
    }
}
