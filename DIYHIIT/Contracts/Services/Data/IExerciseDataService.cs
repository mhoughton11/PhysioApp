using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IExerciseDataService
    {
        Task<IEnumerable<IExercise>> GetAllExercisesAsync(WorkoutType? t = null);
        Task<IExercise> GetExerciseById(int id);
    }
}
