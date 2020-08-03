using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DIYHIIT.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        public ExerciseController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ??
                throw new ArgumentNullException(nameof(appDbContext));
        }

        private readonly ILogger<ExerciseController> _logger;
        private readonly AppDbContext _appDbContext;

        public ExerciseController(ILogger<ExerciseController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //[Route("exercises")]
        //public IEnumerable<Exercise> GetExercises()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new Exercise
        //    {
        //        Name = $"Exercise{rng.Next(0xFF)}",
        //        Index = rng.Next(10)
        //    }).ToArray();
        //}

        [HttpGet]
        [Route("exercises/{id:int?}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            /*
            var exercise = await _appDbContext.Exercises.Where(e => e.ID == id).SingleOrDefaultAsync();
            if (exercise != null)
            {
                return Ok(exercise);
            }
            */

            return NotFound();
        }
    }
}
