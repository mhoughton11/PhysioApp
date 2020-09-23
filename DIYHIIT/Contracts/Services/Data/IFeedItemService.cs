using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Models.ViewComponents;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IFeedItemService
    {
        Task<IEnumerable<FeedItem>> GetAllItems();
        Task<FeedItem> PostFeedItem(FeedItem item);
    }
}
