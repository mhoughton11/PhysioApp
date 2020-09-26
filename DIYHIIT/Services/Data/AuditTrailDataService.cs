using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using DIYHIIT.Constants;
using DIYHIIT.Contracts;
using DIYHIIT.Contracts.Services.Data;
using DIYHIIT.Library.Models.ViewComponents;
using DIYHIIT.Library.Models;
using static DIYHIIT.Library.Settings.Settings;

namespace DIYHIIT.Services.Data
{
    public class AuditTrailDataService : BaseService, IAuditTrailDataService
    {
        public AuditTrailDataService(IGenericRepository genericRepository, IBlobCache blobCache = null)
            : base(blobCache)
        {
            _genericRepository = genericRepository;
        }

        private readonly IGenericRepository _genericRepository;

        public async Task<AuditTrail> PostAuditTrail(AuditTrail item)
        {
            var path = string.Empty;

            switch (App.AppHostOptions)
            {
                case HostOptions.Production:
                    path = ApiConstants.BaseApiUrl + ApiConstants.PostAuditTrailEndpoint;
                    break;

                case HostOptions.LocalHost:
                    path = ApiConstants.BaseLocalHost + ApiConstants.PostAuditTrailEndpoint;
                    break;
            }

            var ex = await _genericRepository.PostAsync<FeedItem>(path, item);

            return ex;
        }
    }
}
