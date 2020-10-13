using System;
using System.Collections.Generic;
using System.Diagnostics;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;
using Newtonsoft.Json;

namespace PhysioApp.Library.Helpers
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

        public static string GetFeedItemBackgroundColour(FeedItemType type)
        {
            var colour = string.Empty;

            switch (type)
            {
                case FeedItemType.Exercise:
                    colour = "#cceeff";
                    break;

                case FeedItemType.Post:
                    colour = "#ffffff";
                    break;

                case FeedItemType.Update:
                    colour = "#d1e0e0";
                    break;

                case FeedItemType.Workout:
                    colour = "#adc2eb";
                    break;
            }

            return colour;
        }
    }
}
