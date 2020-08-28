using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IExerciseDataService
    {
        Task<IEnumerable<IExercise>> GetAllExercisesAsync(int? type = null, HostOptions options = HostOptions.Production);
        Task<IExercise> GetExerciseById(int id);
    }
}
