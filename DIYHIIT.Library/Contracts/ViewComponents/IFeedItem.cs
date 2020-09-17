using System;
namespace DIYHIIT.Library.Contracts.ViewComponents
{
    public interface IFeedItem
    {
        IUser User { get; set; }

        IWorkout Workout { get; set; }
    }
}
