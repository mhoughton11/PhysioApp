using System;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Settings;
using PhysioApp.Library.Models;
using PhysioApp.Library.Contracts.ViewComponents;
using static PhysioApp.Library.Settings.Settings;
using static PhysioApp.Library.Helpers.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PhysioApp.Library.Models.ViewComponents
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
