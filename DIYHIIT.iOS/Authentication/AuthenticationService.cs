using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using DIYHIIT.Authentication;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Contracts.Services.General;
using DIYHIIT.DependencyInjection;
using DIYHIIT.iOS.Authentication;
using Firebase.Auth;
using Xamarin.Forms;

namespace DIYHIIT.iOS.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResponse> LoginWithEmailAndPassword(string userName, string password)
        {
            var userDataService = AppContainer.Container.Resolve<IUserDataService>();

            try
            {
                // Attempt sign in
                var result = await Auth.DefaultInstance.SignInWithPasswordAsync(userName, password);

                // Does user already exist in DB? If not create new.
                var user = await userDataService.GetUser(result.User.Uid);

                if (user != null)
                {
                    userDataService.CurrentUser = user;
                }
                else
                {
                    user.Uid = result.User.Uid;
                    user.Username = result.User.Email;
                    userDataService.CurrentUser = await userDataService.SaveUser(user);
                }

                return new AuthenticationResponse
                {
                    User = user,
                    IsAuthenticated = true,
                    ResponseToken = await result.User.GetIdTokenAsync()
                };
            }
            catch (Exception e)
            {
                userDataService.CurrentUser = null;
                Debug.WriteLine(e.Message);
            }

            return null;
        }
    }
}
