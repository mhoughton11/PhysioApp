using System;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Library.Contracts.ViewComponents
{
    public interface IFeedItem
    {
        int FeedItemKey { get; set; }

        string UserName { get; set; }
        DateTime Date { get; set; }

        FeedItemType FeedType { get; set; }

        string ImageURL { get; set; }

        string Title { get; set; }
        string Message { get; set; }

        string BackgroundColour { get; }

        Workout Workout { get; set; }
    }
}
