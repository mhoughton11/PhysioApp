using System;
using System.Threading.Tasks;
using PhysioApp.Authentication;
using PhysioApp.Library.Models;

namespace PhysioApp.Contracts.Services.General
{
    public interface IAuthenticationService
    {
        AuthenticationResponse AutoLogin();

        Task<AuthenticationResponse> LoginWithEmailAndPassword(string userName, string password);
        Task<AuthenticationResponse> SignUpWithEmailAndPassword(string userName, string password);
        void SignOut();
    }
}
