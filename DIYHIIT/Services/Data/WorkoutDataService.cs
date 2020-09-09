using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Services.Data
{
    public class WorkoutDataService : BaseService, IWorkoutDataService
    {
        private readonly IGenericRepository _genericRepository;

        public WorkoutDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IWorkout> SaveWorkout(IWorkout workout, HostOptions options = HostOptions.Production)
        {
            string path = string.Empty;
        
            switch (options)
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

        public async Task<IEnumerable<IWorkout>> GetWorkoutsAsync(HostOptions options = HostOptions.Production)
        {
            // If cache not empty, fetch from cache and return items.
            //var cacheWorkouts = await GetFromCache<List<Workout>>("Workouts");

            //if (cacheWorkouts != null)
            //{
            //    return cacheWorkouts;
            //}

            string path = string.Empty;

            switch (options)
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
    }
}
