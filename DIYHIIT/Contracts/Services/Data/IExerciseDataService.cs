using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IExerciseDataService
    {
        Task<IEnumerable<DB_Exercise>> GetAllExercisesAsync(int? type = null);
    }
}
