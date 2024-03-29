﻿using System;

namespace PhysioApp.Library.Settings
{
    public class Settings
    {
        public enum HostOptions
        {
            Production = 1,
            LocalHost
        };

        public enum FeedItemType
        {
            Workout = 1,
            Post = 2,
            Exercise = 3,
            Update = 4
        };

        public enum AuditType
        {
            Workout = 1,
            ProfileUpdate = 2
        }

        public enum WorkoutType
        {
            Calisthenics,
            HIIT,
            Pilates,
            Yoga,
            Stretches,
            Mobility
        };
    }
}
