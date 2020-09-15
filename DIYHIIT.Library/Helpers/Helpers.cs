using System;
using System.Collections.Generic;
using System.Diagnostics;
using DIYHIIT.Library.Models;
using Newtonsoft.Json;

namespace DIYHIIT.Library.Helpers
{
    public static class Helpers
    {
        public static int GetWorkoutDuration(List<Exercise> exercises, double active, double rest)
        {
            if (exercises.Count == 0)
                return 0;

            double seconds = (active * exercises.Count) + (rest * exercises.Count - 1);

            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.Minutes;
        }

        public static string GetWorkoutCountString(List<Exercise> exercises)
        {
            if (exercises.Count == 0) { return "0 Moves"; }

            if (exercises.Count == 1)
                return "1 Move";

            return $"{exercises.Count} Moves";
        }
    }
}
