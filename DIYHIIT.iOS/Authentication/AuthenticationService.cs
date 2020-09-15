using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DIYHIIT.Authentication;
using DIYHIIT.Contracts.Services.General;
using Firebase.Auth;
using Foundation;

namespace DIYHIIT.iOS.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResponse AutoLogin()
        {
            if (Auth.DefaultInstance.CurrentUser != null)
            {
                return new AuthenticationResponse
                {
                    UserUid = Auth.DefaultInstance.CurrentUser.Uid,
                    IsAuthenticated = true,
                };
            }

            else
            {
                return new AuthenticationResponse
                {
                    UserUid = null,
                    IsAuthenticated = false
                };
            }
        }

        public void SignOut()
        {
            NSError error;
            Auth.DefaultInstance.SignOut(out error);
        }

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

        public async Task<AuthenticationResponse> SignUpWithEmailAndPassword(string userName, string password)
        {
            try
            {
                var result = await Auth.DefaultInstance.CreateUserAsync(userName, password);

                if (result != null)
                {
                    return new AuthenticationResponse
                    {
                        IsAuthenticated = true,
                        ResponseToken = result.User.GetIdTokenAsync(false).ToString(),
                        UserUid = result.User.Uid
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Sign up failed.");
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
