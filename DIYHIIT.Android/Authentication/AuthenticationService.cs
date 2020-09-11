using System;
using System.Threading.Tasks;
using DIYHIIT.Authentication;
using DIYHIIT.Contracts.Services.General;
using Firebase.Auth;

namespace DIYHIIT.Droid.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResponse> LoginWithEmailAndPassword(string userName, string password)
        {
            var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(userName, password);
            return new AuthenticationResponse()
            {
                ResponseToken = (await user.User.GetIdTokenAsync(false)).Token
            };
        }

        public bool IsUserAuthenticated()
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> Register(string firstName, string lastName, string email, string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
