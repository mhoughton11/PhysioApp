using System;

namespace DIYHIIT.Constants
{
    public class ApiConstants
    {
        // Base
        public const string BaseLocalHost = "http://localhost:45283/api/";
        public const string BaseApiUrl = "https://diyhiit-api.azurewebsites.net/api/";

        // Exercises
        public const string ExercisesEndpoint = "exercise/exercises";
        public const string ExercisesByListEndpoint = "exercise/list";
        public const string ExerciseByIdEndpoint = "exercise/exercises";

        // Users
        public const string GetUsersEndpoint = "user/users";
        public const string GetUserEndpoint = "user/user";
        public const string SaveUserEndpoint = "user/save";
        public const string UpdateUserEndpoint = "user/update";
        public const string UpdateWorkoutEndpoint = "user/updateWorkout";
        public const string SaveWorkoutEndpoint = "user/saveWorkout";
    }
}
