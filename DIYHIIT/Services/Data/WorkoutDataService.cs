using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;

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

        public async Task<IWorkout> SaveWorkout(IWorkout workout)
        {
            var path = ApiConstants.BaseApiUrl + ApiConstants.SaveWorkoutEndpoint;

            await _genericRepository.PostAsync(path, workout);

            return workout;
        }

        public Task<IWorkout> GetWorkoutById(int id)
        {
            IWorkout w = new Workout();

            return Task.Run(() => w);
        }

        public Task<IEnumerable<IWorkout>> GetWorkoutsAsync()
        {
            IEnumerable<IWorkout> w = new List<Workout>();

            return Task.Run(() => w);
        }
    }
}
