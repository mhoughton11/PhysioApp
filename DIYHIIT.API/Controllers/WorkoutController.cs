using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DIYHIIT.API.Controllers
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
            var items = await _appDbContext.DB_Workouts
                                           .OrderBy(w => w.Id)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("userWorkouts")]
        public async Task<ActionResult> GetUserWorkouts(int userId)
        {
            if (!_appDbContext.DB_Users.Any(u => u.Id == userId))
            {
                return NotFound();
            }

            var workouts = await _appDbContext.DB_Workouts.Where(w => w.UserId == userId).OrderBy(w => w.Name).ToListAsync();

            return Ok(workouts);
        }

        [HttpPost]
        [Route("updateWorkout")]
        public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
        {
            _appDbContext.DB_Workouts.Update(workout);
            await _appDbContext.SaveChangesAsync();

            return Ok(workout);
        }

        [HttpPost]
        [Route("saveWorkout")]
        public async Task<IActionResult> SaveWorkout([FromBody] Workout workout)
        {
            // If already contains workout, don't save.
            if (_appDbContext.DB_Workouts.Any(w => w.Id == workout.Id))
            {
                return BadRequest();
            }

            _appDbContext.DB_Workouts.Add(workout);
            await _appDbContext.SaveChangesAsync();

            return Ok(workout);
        }
    }
}