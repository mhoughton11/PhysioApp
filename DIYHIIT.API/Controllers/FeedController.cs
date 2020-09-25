using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Contracts;
using DIYHIIT.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DIYHIIT.Library.Models.ViewComponents;
using Newtonsoft.Json;

namespace DIYHIIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        public FeedController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ??
                throw new ArgumentNullException(nameof(appDbContext));
        }

        private readonly AppDbContext _appDbContext;

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _appDbContext.FeedItems
                                           .OrderBy(e => e.Date)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> PostItem([FromBody]FeedItem feedItem)
        {
            if (feedItem == null)
            {
                return BadRequest();
            }

            _appDbContext.Add(feedItem);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateItem([FromBody] FeedItem updatedItem)
        {
            if (updatedItem == null)
            {
                return BadRequest();
            }

            if (!_appDbContext.FeedItems.Any(i => i.FeedItemKey == updatedItem.FeedItemKey))
            {
                return NotFound();
            }

            var entity = _appDbContext.FeedItems.Attach(updatedItem);
            entity.State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();

            return Ok(entity);
        }
    }
}
