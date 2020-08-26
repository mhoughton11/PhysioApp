using System;
using System.Threading.Tasks;
using DIYHIIT.Auth;
using DIYHIIT.Contracts.Services.General;

namespace DIYHIIT.Services.General.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            var response = new AuthenticationResponse();
            response.IsAuthenticated = true;

            return await Task.Run(() => response);
        }

        public bool IsUserAuthenticated()
        {
            return true;
        }

        public async Task<AuthenticationResponse> Register(string firstName, string lastName, string email, string userName, string password)
        {
            var response = new AuthenticationResponse();
            response.IsAuthenticated = true;

            return await Task.Run(() => response);
        }
    }
}
