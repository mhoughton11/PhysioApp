using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PhysioApp.Authentication;
using PhysioApp.Contracts.Services.General;
using Firebase.Auth;

namespace PhysioApp.Droid.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResponse AutoLogin()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                Debug.WriteLine("Firebase auto-login failed.");

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

        public void SignOut()
        {
            FirebaseAuth.Instance.SignOut();
        }

        public async Task<AuthenticationResponse> SignUpWithEmailAndPassword(string userName, string password)
        {
            try
            {
                var result = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(userName, password);

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
