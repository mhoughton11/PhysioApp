using System;
using DIYHIIT.Library.Settings;

namespace DIYHIIT.Library.Contracts.ViewComponents
{
    public interface IFeedItem
    {
        string UserName { get; set; }
        DateTime DateTime { get; set; }

        //FeedItemType FeedType { get; set; }

        string ImageURL { get; set; }

        string Title { get; set; }
        string Message { get; set; }

        IWorkout Workout { get; set; }
    }
}
