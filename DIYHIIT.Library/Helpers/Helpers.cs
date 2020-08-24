using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Helpers
{
    public static class Helpers
    {
        public static int GetWorkoutDuration(Workout workout)
        {
            if (workout.Exercises.Count == 0)
                return 0;

            double seconds = (workout.ActiveInterval * workout.Exercises.Count)
                + (workout.RestInterval * workout.Exercises.Count - 1) ?? 0;

            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.Minutes;
        }
    }
}
