using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Contracts.Services.Data
{
    public interface IExerciseDataService
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync(int? type = null);
        Task<IEnumerable<Exercise>> GetExercisesFromList(string ids);
    }
}
