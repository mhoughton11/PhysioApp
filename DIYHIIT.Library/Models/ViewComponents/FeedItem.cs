using System;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Contracts.ViewComponents;

namespace DIYHIIT.Library.Models.ViewComponents
{
    public class FeedItem : IFeedItem
    {
        public IUser User { get; set; }
        public IWorkout Workout { get; set; }
    }
}
