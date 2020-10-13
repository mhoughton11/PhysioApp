using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public class ExerciseDataService : BaseService, IExerciseDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ExerciseDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync(int? t = null)
        {
            // If cache not empty, fetch from cache and return items.
            var cacheExercises = await GetFromCache<List<Exercise>>("Exercises");
        
            if (cacheExercises != null)
            {
                if (t == null || t == 0)
                {
                    return cacheExercises;
                }
                else
                {
                    return cacheExercises.Where(e => (int)e.Type == t);
                }
            }
            else
            {
                // Cache empty so get exercises from API as List<Ex.>
                string path = string.Empty;

                switch (App.AppHostOptions)
                {
                    case HostOptions.Production:
                        path = ApiConstants.BaseApiUrl + ApiConstants.ExercisesEndpoint;
                        break;

                    case HostOptions.LocalHost:
                        path = ApiConstants.BaseLocalHost + ApiConstants.ExercisesEndpoint;
                        break;
                }

                var ex = await _genericRepository.GetAsync<List<Exercise>>(path);

                // If exercises null, return. If not, save to cache and return them.
                if (ex == null) { return null; }

                await Cache.InsertObject("Exercises", ex, DateTime.Now.AddMinutes(2));

                if (t == null || t == 0)
                {
                    return ex;
                }
                else
                {
                    return ex.Where(e => (int)e.Type == t);
                }
            }
        }

        public async Task<IEnumerable<Exercise>> GetExercisesFromList(string ids)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.ExercisesByListEndpoint + $"?ids={ids}";
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.ExercisesByListEndpoint + $"?ids={ids}";
                    break;
            }

            var ex = await _genericRepository.GetAsync<List<Exercise>>(path);

            return ex;
        }
    }
}
