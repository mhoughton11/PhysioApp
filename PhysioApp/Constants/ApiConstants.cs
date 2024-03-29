﻿using System;

namespace PhysioApp.Constants
{
    public class ApiConstants
    {
        // Base
        public const string BaseLocalHost = "http://localhost:45283/api/";
        public const string BaseApiUrl = "https://PhysioApp-api.azurewebsites.net/api/";

        // Exercises
        public const string ExercisesEndpoint = "exercise/exercises";
        public const string ExercisesByListEndpoint = "exercise/list";
        public const string ExerciseByIdEndpoint = "exercise/exercises";

        // Workouts
        public const string SaveWorkoutEndpoint = "workout/save";
        public const string GetWorkoutsEndpoint = "workout/workouts";
        public const string GetUserWorkoutsEndpoint = "workout/user";
        public const string UpdateWorkoutEndpoint = "workout/update";

        // Users
        public const string GetUsersEndpoint = "user/users";
        public const string GetUserEndpoint = "user/user";
        public const string SaveUserEndpoint = "user/save";
        public const string UpdateUserEndpoint = "user/update";

        // Feed Items
        public const string FeedItemsEndpoint = "feed/all";
        public const string PostFeedItemEndpoint = "feed/post";

        // Audit Trails
        public const string PostAuditTrailEndpoint = "audittrails/post";
    }
}
