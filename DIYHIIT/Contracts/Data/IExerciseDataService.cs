using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Models.Exercise;

namespace DIYHIIT.Contracts.Data
{
    public interface IExerciseDataService
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseById(int id);
    }
}
