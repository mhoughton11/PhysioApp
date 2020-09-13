using System;
using System.Threading.Tasks;
using DIYHIIT.Authentication;
using DIYHIIT.Contracts.Services.General;
using Firebase.Auth;

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
