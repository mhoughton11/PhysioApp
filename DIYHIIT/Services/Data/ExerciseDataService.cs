using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Data;
using DIYHIIT.Models.Exercise;

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

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            var cacheExercises = await GetFromCache<List<Exercise>>("AllExercises");

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

                var ex = await _genericRepository.GetAsync<List<Exercise>>(builder.ToString());

                await Cache.InsertObject("AllExercicses", ex, DateTime.Now.AddMinutes(2));

                return ex;
            }
        }

        public Task<Exercise> GetExerciseById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
