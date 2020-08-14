using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Models;

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

        public async Task<IEnumerable<IExercise>> GetAllExercisesAsync(WorkoutType? t = null)
        {
            var cacheExercises = new List<IExercise>();

            if (t == null)
            {
                cacheExercises = await GetFromCache<List<IExercise>>("AllExercises");
            }

            if (cacheExercises != null)
            {
                return cacheExercises;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.ExercisesEndpoint
                };

                var ex = await _genericRepository.GetAsync<List<IExercise>>(builder.ToString());
                
                if (t != null)
                {
                    var typeName = Enum.GetName(typeof(WorkoutType), t);
                    var cacheName = $"All{typeName}Exercises";

                    ex = ex.Where(e => e.Type == t).ToList();
                    await Cache.InsertObject(cacheName, ex, DateTime.Now.AddMinutes(2));
                }
                else
                {
                    await Cache.InsertObject("AllExercicses", ex, DateTime.Now.AddMinutes(2));
                }

                return ex;
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
                    Path = ApiConstants.ExerciseByIdEndpoint + $"{id}"
                };

                var ex = await _genericRepository.GetAsync<Exercise>(builder.ToString());

                await Cache.InsertObject($"{id}", ex, DateTime.Now.AddMinutes(2));

                return ex;
            }
        }
    }
}
