using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Contracts;

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

        public Task<IWorkout> AddWorkout(IWorkout workout)
        {
            
        }

        public Task<IWorkout> GetWorkoutById(int id)
        {
            
        }

        public Task<IEnumerable<IWorkout>> GetWorkoutsAsync()
        {
            
        }
    }
}
