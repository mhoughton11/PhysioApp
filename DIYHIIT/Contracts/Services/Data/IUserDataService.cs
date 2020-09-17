using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.Data
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
