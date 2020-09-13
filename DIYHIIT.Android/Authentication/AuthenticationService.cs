using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DIYHIIT.Authentication;
using DIYHIIT.Contracts.Services.General;
using Firebase.Auth;

namespace DIYHIIT.Droid.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResponse AutoLogin()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return new AuthenticationResponse
                {
                    UserUid = FirebaseAuth.Instance.CurrentUser.Uid,
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

        public async Task<AuthenticationResponse> LoginWithEmailAndPassword(string userName, string password)
        {
            AuthenticationResponse response;

            try
            {
                // Attempt sign in
                var result = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(userName, password);

                if (await result.User.GetIdTokenAsync(false) != null)
                {
                    Debug.WriteLine("Firebase auth suuccess");
                    Debug.WriteLine(result.User.Uid);
                }

                var token = await result.User.GetIdTokenAsync(false);
                Debug.WriteLine($"Auth token: {token}");

                response = new AuthenticationResponse
                {
                    UserUid = result.User.Uid,
                    IsAuthenticated = true,
                    ResponseToken = (await result.User.GetIdTokenAsync(false)).ToString()
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
