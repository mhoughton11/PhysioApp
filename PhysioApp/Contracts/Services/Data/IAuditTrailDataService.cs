using System.Collections.Generic;
using System.Threading.Tasks;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;

namespace PhysioApp.Contracts.Services.Data
{
    public interface IAuditTrailDataService
    {
        Task<AuditTrail> PostAuditTrail(AuditTrail auditTrail);
    }
}
