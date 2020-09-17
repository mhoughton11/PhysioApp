using System.Collections.Generic;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Library.Contracts
{
    public interface IUser : IEntity
    {
        string Uid { get; set; }

        string Username { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }

        ICollection<Workout> Workouts { get; set; }
    }
}
