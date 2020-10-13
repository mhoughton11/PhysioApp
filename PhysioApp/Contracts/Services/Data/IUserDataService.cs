using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Models;

namespace PhysioApp.Contracts.Services.Data
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string uid);
        Task<User> SaveUser(User user);
        Task<User> UpdateUser(User user);
        Task<Workout> SaveWorkout(Workout workout);
        Task<Workout> UpdateWorkout(Workout workout);
    }
}
