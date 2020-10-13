using System;
using PhysioApp.Library.Models;

namespace PhysioApp.Library.Contracts
{
    public interface IUserSettings
    {
        bool PostToFeed { get; set; }
    }
}
