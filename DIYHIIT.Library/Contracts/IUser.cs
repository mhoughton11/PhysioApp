using System;
namespace DIYHIIT.Library.Contracts
{
    public interface IUser
    {
        string Uid { get; set; }

        string Username { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }


    }
}
