using System;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Settings;
using DIYHIIT.Library.Contracts.ViewComponents;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class FeedItem : IFeedItem
    {
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }

        public string ImageURL { get; set; }

        //public FeedItemType FeedType { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public IWorkout Workout { get; set; }
    }
}
