using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DIYHIIT.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Get()
        {
            var items = await _appDbContext.Users
                                           .OrderBy(u => u.Username)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser(string uid)
        {
            if (uid == null)
            {
                return NotFound();
            }

            // Retrieve user
            var user = await _appDbContext.Users
                                          .Where(u => u.Uid == uid)
                                          //.Include("Workouts")
                                          .SingleOrDefaultAsync();

            // Retrieve user workouts
            user.Workouts = await _appDbContext.Workouts
                                               .Where(w => w.UserKey == user.UserKey)
                                               .ToListAsync();

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveUser([FromBody]User user)
        {
            if (user == null) { return NoContent(); }

            // Check if username already exists.
            if (_appDbContext.Users.Any(u => u.Uid == user.Uid))
            {
                return Forbid();
            }

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!_appDbContext.Users.Any(u => u.Username == user.Username))
            {
                return NotFound();
            }

            var entity = _appDbContext.Users.Attach(user);
            entity.State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
