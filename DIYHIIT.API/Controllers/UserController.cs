using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Models;
using DIYHIIT.Library.Persistance.Models;
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
            var items = await _appDbContext.DB_Users
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

            var user = await _appDbContext.DB_Users
                                          .Where(u => u.Uid == uid)
                                          .SingleOrDefaultAsync();

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
            if (_appDbContext.DB_Users.Any(u => u.Uid == user.Uid))
            {
                return Forbid();
            }

            _appDbContext.DB_Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            _appDbContext.DB_Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        [Route("updateWorkout")]
        public async Task<IActionResult> UpdateWorkout([FromBody]Workout workout)
        {
            // Get associated parent user
            var user = await _appDbContext.DB_Users.Where(u => u.Id == workout.UserId).SingleOrDefaultAsync();

            // Does workout already exist in user
            if(!user.Workouts.Any(w => w.Id == workout.Id))
            {
                // Workout exists so update
                var _workout = user.Workouts.Where(w => w.Id == workout.Id).SingleOrDefault();
                _workout = workout;
            }
            else
            {
                // Workout doesn't exist in user so create
                user.Workouts.Add(workout);
            }

            _appDbContext.DB_Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            return Ok(workout);
        }
    }
}
