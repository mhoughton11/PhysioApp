using System;

namespace DIYHIIT.Constants
{
    public class ApiConstants
    {
        // Base
        public const string BaseApiUrl = "https://diyhiit-api.azurewebsites.net/api/";

        // Exercises
        public const string ExercisesEndpoint = "exercise/exercises";
        public const string ExerciseByIdEndpoint = "exercise/exercises";

        // Workouts
        public const string SaveWorkoutEndpoint = "workout/save";

        // Users
        public const string GetUsersEndpoint = "user/users";
        public const string GetUserEndpoint = "user/user";
        public const string SaveUserEndpoint = "user/save";
    }
}
