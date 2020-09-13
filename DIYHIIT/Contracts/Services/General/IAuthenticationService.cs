using System;
using System.Threading.Tasks;
using DIYHIIT.Authentication;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.General
{
    public interface IAuthenticationService
    {
        AuthenticationResponse AutoLogin();

        Task<AuthenticationResponse> LoginWithEmailAndPassword(string userName, string password);
    }
}
