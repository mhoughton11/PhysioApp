using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly AppDbContext _appDbContext;

        [HttpGet]
        [Route("exercises")]
        public async Task<IActionResult> GetExercises()
        {
            var items = await _appDbContext.Exercises
                                           .OrderBy(e => e.DisplayName)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [Route("exercises/{id:int?}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            
            var exercise = await _appDbContext.Exercises
                                              .Where(e => e.ID == id)
                                              .SingleOrDefaultAsync();
            if (exercise != null)
            {
                return Ok(exercise);
            }

            return NotFound();
        }
    }
}
