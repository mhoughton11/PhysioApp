using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IUserSettings : IEntity
    {
        bool PostToFeed { get; set; }

        User User { get; set; }
        int UserKey { get; set; }
    }
}
