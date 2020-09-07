using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Library.Models;
using Newtonsoft.Json;

namespace DIYHIIT.Library.Helpers
{
    public static class Helpers
    {
        public static int GetWorkoutDuration(Workout workout)
        {
            if (workout.ExerciseIDs == null) { return 0; }

            var exercises = JsonConvert.DeserializeObject<List<int>>(workout.ExerciseIDs);
            if (exercises.Count == 0)
                return 0;

            double seconds = (workout.ActiveInterval * exercises.Count)
                + (workout.RestInterval * exercises.Count - 1) ?? 0;

            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.Minutes;
        }
    }
}
