using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        private readonly AppDbContext _appDbContext;

        [HttpGet]
        [Route("exercises")]
        public async Task<IActionResult> GetExercises()
        {
            var items = await _appDbContext.ExerciseCatalog
                                           .OrderBy(e => e.DisplayName)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetExercisesFromList(string ids)
        {
            var exercises = new List<Exercise>();
            var exerciseIds = JsonConvert.DeserializeObject<List<int>>(ids);

            if (exerciseIds != null)
            {
                foreach (var id in exerciseIds)
                {
                    var ex = await _appDbContext.ExerciseCatalog
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();

                    exercises.Add(ex);
                }

                return Ok(exercises);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("exercise")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            
            var exercise = await _appDbContext.ExerciseCatalog
                                              .Where(e => e.Id == id)
                                              .SingleOrDefaultAsync();
            if (exercise != null)
            {
                return Ok(exercise);
            }

            return NotFound();
        }
    }
}
