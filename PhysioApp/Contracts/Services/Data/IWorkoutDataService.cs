using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Contracts;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Contracts.Services.Data
{
    public interface IWorkoutDataService
    {
        Task<IEnumerable<IWorkout>> GetWorkoutsAsync();
        Task<IWorkout> SaveWorkout(IWorkout workout);
        Task<IWorkout> GetWorkoutById(int id);
        Task<IEnumerable<IWorkout>> GetWorkoutsForUser(int userId);
        Task<IWorkout> UpdateWorkout(IWorkout workout);
    }
}