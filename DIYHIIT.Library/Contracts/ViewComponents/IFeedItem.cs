using System;
using DIYHIIT.Library.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Library.Contracts.ViewComponents
{
    public interface IFeedItem : IEntity
    {
        string UserName { get; set; }
        DateTime Date { get; set; }

        FeedItemType FeedType { get; set; }

        string ImageURL { get; set; }

        string Title { get; set; }
        string Message { get; set; }

        string BackgroundColour { get; set; }

        Workout Workout { get; set; }
    }
}
