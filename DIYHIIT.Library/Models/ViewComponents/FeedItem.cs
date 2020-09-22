using System;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Settings;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Contracts.ViewComponents;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class FeedItem : IFeedItem
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public string ImageURL { get; set; }

        public FeedItemType FeedType { get; set; }

        public string BackgroundColour { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public Workout Workout { get; set; }
    }
}
