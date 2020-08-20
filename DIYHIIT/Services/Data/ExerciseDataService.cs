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

        public async Task<IEnumerable<IExercise>> GetAllExercisesAsync(string t = null)
        {
            var cacheExercises = new List<Exercise>();

            if (t == null)
            {
                cacheExercises = await GetFromCache<List<Exercise>>("AllExercises");
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

                var path = ApiConstants.BaseApiUrl + ApiConstants.ExercisesEndpoint;

                var ex = await _genericRepository.GetAsync<List<Exercise>>(path);

                if (ex == null) { return null; }
                
                if (t != null)
                {
                    var cacheName = $"All{t}Exercises";

                    ex = ex.Where(e => e.Type.ToString() == t).ToList();
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
                    Path = ApiConstants.ExerciseByIdEndpoint + $"/{id}"
                };

                var ex = await _genericRepository.GetAsync<Exercise>(builder.ToString());

                await Cache.InsertObject($"{id}", ex, DateTime.Now.AddMinutes(2));

                return ex;
            }
        }
    }
}
