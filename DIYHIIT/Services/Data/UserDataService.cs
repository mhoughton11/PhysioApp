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
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Services.Data
{
    public class UserDataService : BaseService, IUserDataService
    {
        private readonly IGenericRepository _genericRepository;

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
                var path = string.Empty;

                switch (App.AppHostOptions)
                {
                    case HostOptions.LocalHost:
                        path = ApiConstants.BaseLocalHost
                            + ApiConstants.GetUserEndpoint + $"?uid={uid}";
                        break;

                    case HostOptions.Production:
                        path = ApiConstants.BaseApiUrl
                            + ApiConstants.GetUserEndpoint + $"?uid={uid}";
                        break;
                }

                var user = await _genericRepository.GetAsync<User>(path);

                // If user null, return. If not, save to cache and return.
                if (user == null) { return null; }

                await Cache.InsertObject($"User{uid}", user, DateTime.Now.AddSeconds(60));

                return user;
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
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.SaveUserEndpoint;
                    break;

                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.SaveUserEndpoint;
                    break;
            }

            await _genericRepository.PostAsync(path, user);

            return user;
        }

        public async Task<Workout> UpdateWorkout(Workout workout)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.UpdateWorkoutEndpoint;
                    break;

                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.UpdateWorkoutEndpoint;
                    break;
            }

            if (workout.UserID == 0) { return null; }

            return await _genericRepository.PostAsync(path, workout);
        }

        public async Task<User> UpdateUser(User user)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.UpdateUserEndpoint;
                    break;

                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.UpdateUserEndpoint;
                    break;
            }

            return await _genericRepository.PostAsync(path, user);
        }

        public async Task<Workout> SaveWorkout(Workout workout)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.SaveWorkoutEndpoint;
                    break;

                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.SaveWorkoutEndpoint;
                    break;
            }

            return await _genericRepository.PostAsync(path, workout);
        }
    }
}
