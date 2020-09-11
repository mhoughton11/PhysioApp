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
           AuthenticationResponse response;

            try
            {
                // Attempt sign in
                var result = await Auth.DefaultInstance.SignInWithPasswordAsync(userName, password);

                response = new AuthenticationResponse
                {
                    UserUid = result.User.Uid,
                    IsAuthenticated = true,
                    ResponseToken = await result.User.GetIdTokenAsync()
                };
            }
            catch (Exception)
            {
                response = new AuthenticationResponse
                {
                    IsAuthenticated = false,
                    ResponseToken = null
                };
            }

            return response;
        }
    }
}
