using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> SaveUser(User user);
    }
}
