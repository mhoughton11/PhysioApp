using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using PhysioApp.Constants;
using PhysioApp.Contracts;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Services.Data
{
    public class WorkoutDataService : BaseService, IWorkoutDataService
    {
        private readonly IGenericRepository _genericRepository;

        public WorkoutDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IWorkout> SaveWorkout(IWorkout workout)
        {
            string path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.SaveWorkoutEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.SaveWorkoutEndpoint;
                    break;
            }

            await _genericRepository.PostAsync(path, workout);

            return workout;
        }

        public Task<IWorkout> GetWorkoutById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IWorkout>> GetWorkoutsAsync()
        {
            string path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.GetWorkoutsEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.GetWorkoutsEndpoint;
                    break;
            }

            var workouts = await _genericRepository.GetAsync<List<Workout>>(path);

            await Cache.InsertObject("Workouts", workouts, DateTime.Now.AddMinutes(2));

            return workouts;
        }

        public async Task<IWorkout> UpdateWorkout(IWorkout workout)
        {
            string path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.UpdateWorkoutEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.UpdateWorkoutEndpoint;
                    break;
            }

            return await _genericRepository.PostAsync(path, workout);
        }

        public async Task<IEnumerable<IWorkout>> GetWorkoutsForUser(int userId)
        {
            string path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.GetUserWorkoutsEndpoint + $"?userId={userId}"; ;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.GetUserWorkoutsEndpoint + $"?userId={userId}"; ;
                    break;
            }

            return await _genericRepository.GetAsync<List<Workout>>(path);
        }
    }
}