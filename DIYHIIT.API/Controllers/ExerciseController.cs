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
            var items = await _appDbContext.Exercises
                                           .Where(e => e.WorkoutKey == 1)
                                           .OrderBy(e => e.DisplayName)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetFromString(string ids)
        {
            var exercises = new List<Exercise>();
            var _ids = JsonConvert.DeserializeObject<List<int>>(ids);

            if (_ids != null && _ids.Count > 0)
            {
                foreach (var id in _ids)
                {
                    var ex = await _appDbContext.Exercises
                        .Where(e => e.ExerciseKey == id)
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
    }
}
