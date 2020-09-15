using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Services.Data
{
    public class ExerciseDataService : BaseService, IExerciseDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ExerciseDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<IExercise>> GetAllExercisesAsync(int? t = null)
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

                var ex = await _genericRepository.GetAsync<List<DB_Exercise>>(path);

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

        public async Task<IExercise> GetExerciseById(int id)
        {
            var cacheExercise = await GetFromCache<Exercise>(id.ToString());
            if (cacheExercise != null)
            {
                return cacheExercise;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.ExerciseByIdEndpoint + $"/{id}"
                };

                var ex = await _genericRepository.GetAsync<Exercise>(builder.ToString());

                await Cache.InsertObject($"{id}", ex, DateTime.Now.AddMinutes(2));

                return ex;
            }
        }

        public async Task<IEnumerable<IExercise>> GetExercisesFromList(string exerciseList)
        {
            string path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost;
                    break;
            }

            path += ApiConstants.ExercisesByListEndpoint + $"?ids={exerciseList}";

            //UriBuilder builder = new UriBuilder()
            //{
            //    Path = path + ApiConstants.ExercisesByListEndpoint,
            //    Query = $"?ids={exerciseList}"
            //};

            var ex = await _genericRepository.GetAsync<List<Exercise>>(path);

            // If exercises null, return. If not, save to cache and return them.
            if (ex == null) { return null; }

            return ex;
        }
    }
}
