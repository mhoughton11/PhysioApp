using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IWorkoutDataService
    {
        Task<IEnumerable<IWorkout>> GetWorkoutsAsync();
        Task<IWorkout> SaveWorkout(IWorkout workout);
        Task<IWorkout> GetWorkoutById(int id);
    }
}
