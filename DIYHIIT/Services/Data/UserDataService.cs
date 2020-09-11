using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Services.Data
{
    public class UserDataService : BaseService, IUserDataService
    {
        private readonly IGenericRepository _genericRepository;

        public User CurrentUser { get; set; }

        public User GetCurrentUser() => CurrentUser;

        public UserDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<User> GetUser(string uid)
        {
            var cacheUser = await GetFromCache<User>($"User{uid}");

            if (cacheUser != null)
            {
                return cacheUser;
            }
            else
            {
                var path = ApiConstants.BaseApiUrl
                    + ApiConstants.GetUserEndpoint + $"?uid={uid}";

                var user = _genericRepository.GetAsync<User>(path);

                // If user null, return. If not, save to cache and return.
                if (user == null) { return null; }

                await Cache.InsertObject($"User{uid}", user, DateTime.Now.AddSeconds(60));

                return await user;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var cacheUsers = await GetFromCache<List<User>>($"Users");

            if (cacheUsers != null)
            {
                return cacheUsers;
            }
            else
            {
                var path = ApiConstants.BaseApiUrl + ApiConstants.GetUsersEndpoint;

                var users = _genericRepository.GetAsync<List<User>>(path);

                // If user null, return. If not, save to cache and return.
                if (users == null) { return null; }

                await Cache.InsertObject($"Users", users, DateTime.Now.AddSeconds(60));

                return await users;
            }
        }

        public async Task<User> SaveUser(User user)
        {
            var path = ApiConstants.BaseApiUrl + ApiConstants.SaveUserEndpoint;

            await _genericRepository.PostAsync(path, user);

            return user;
        }
    }
}
