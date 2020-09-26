using System.Collections.Generic;
using System.Threading.Tasks;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;

namespace DIYHIIT.Contracts.Services.Data
{
    public interface IAuditTrailDataService
    {
        Task<AuditTrail> PostAuditTrail(AuditTrail auditTrail);
    }
}
