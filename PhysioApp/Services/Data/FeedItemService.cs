using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using PhysioApp.Constants;
using PhysioApp.Contracts;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Library.Models.ViewComponents;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Services.Data
{
    public class FeedItemService : BaseService, IFeedItemService
    {
        public FeedItemService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        private readonly IGenericRepository _genericRepository;

        public async Task<IEnumerable<FeedItem>> GetAllItems()
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.FeedItemsEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.FeedItemsEndpoint;
                    break;
            }

            var ex = await _genericRepository.GetAsync<List<FeedItem>>(path);

            return ex;
        }

        public async Task<FeedItem> PostFeedItem(FeedItem item)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.PostFeedItemEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.PostFeedItemEndpoint;
                    break;
            }

            var ex = await _genericRepository.PostAsync<FeedItem>(path, item);

            return ex;
        }
    }
}
