using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PhysioApp.API.Models;
using PhysioApp.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhysioApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public WorkoutController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ??
                throw new ArgumentNullException(nameof(appDbContext));
        }

        [HttpGet]
        [Route("workouts")]
        public async Task<IActionResult> GetWorkouts()
        {
            var items = await _appDbContext.Workouts
                                           .OrderBy(w => w.Name)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult> GetUserWorkouts(int userId)
        {
            if (!_appDbContext.Users.Any(u => u.ID == userId))
            {
                return NotFound();
            }

            var workouts = await _appDbContext.Workouts.Where(w => w.UserID == userId)
                                                       .Include(w => w.Exercises)
                                                       .ToListAsync();

            return Ok(workouts);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
        {
            if (workout == null)
            {
                return BadRequest();
            }

            if (!_appDbContext.Workouts.Any(w => w.ID == workout.ID))
            {
                return NotFound();
            }

            var entity = _appDbContext.Workouts.Attach(workout);
            entity.State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveWorkout([FromBody] Workout workout)
        {
            // If already contains workout, don't save.
            if (_appDbContext.Workouts.Any(w => w.ID == workout.ID))
            {
                return BadRequest();
            }

            _appDbContext.Workouts.Add(workout);
            await _appDbContext.SaveChangesAsync();

            return Ok(workout);
        }
    }
}