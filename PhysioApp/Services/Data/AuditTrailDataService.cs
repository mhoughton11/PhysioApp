using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using PhysioApp.Constants;
using PhysioApp.Contracts;
using PhysioApp.Contracts.Services.Data;
using PhysioApp.Library.Models.ViewComponents;
using PhysioApp.Library.Models;
using static PhysioApp.Library.Settings.Settings;

namespace PhysioApp.Services.Data
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

            var ex = await _genericRepository.PostAsync<AuditTrail>(path, item);

            return ex;
        }
    }
}
