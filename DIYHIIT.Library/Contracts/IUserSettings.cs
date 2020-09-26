using System;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IUserSettings
    {
        bool PostToFeed { get; set; }
    }
}
