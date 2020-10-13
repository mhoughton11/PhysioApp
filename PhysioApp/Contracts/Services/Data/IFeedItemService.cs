using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using PhysioApp.Library.Models.ViewComponents;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Contracts.Services.Data
{
    public interface IFeedItemService
    {
        Task<IEnumerable<FeedItem>> GetAllItems();
        Task<FeedItem> PostFeedItem(FeedItem item);
    }
}
