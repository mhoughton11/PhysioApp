using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PhysioApp.API.Models;
using PhysioApp.Library.Contracts;
using PhysioApp.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysioApp.Library.Models.ViewComponents;
using Newtonsoft.Json;

namespace PhysioApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditTrailsController : ControllerBase
    {
        public AuditTrailsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ??
                throw new ArgumentNullException(nameof(appDbContext));
        }

        private readonly AppDbContext _appDbContext;

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _appDbContext.AuditTrails
                                           .OrderByDescending(at => at.DOE)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> PostItem([FromBody] AuditTrail auditTrail)
        {
            if (auditTrail == null)
            {
                return BadRequest();
            }

            _appDbContext.AuditTrails.Add(auditTrail);
            await _appDbContext.SaveChangesAsync();

            return Ok(auditTrail);
        }
    }
}
