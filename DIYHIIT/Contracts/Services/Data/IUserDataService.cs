using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IUserDataService
    {
        User CurrentUser { get; set; }
        User GetCurrentUser();

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string uid);
        Task<User> SaveUser(User user);
    }
}
