using System;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Settings;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Contracts.ViewComponents;
using static DIYHIIT.Library.Settings.Settings;
using static DIYHIIT.Library.Helpers.Helpers;
using System.ComponentModel.DataAnnotations;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class FeedItem : IFeedItem
    {
        public FeedItem()
        {
            Date = DateTime.Now;
        }

        [Key]
        public int FeedItemKey { get; set; }

        public string UserName { get; set; }
        public DateTime Date { get; set; }

        public string ImageURL { get; set; }

        public FeedItemType FeedType { get; set; }

        public string BackgroundColour => GetFeedItemBackgroundColour(FeedType);

        public string Title { get; set; }
        public string Message { get; set; }

        public Workout Workout { get; set; }
    }
}
