using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIYHIIT.Models.Exercise;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DIYHIIT.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Chest", "Legs", "Arms", "Abs"
        };

        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(ILogger<ExerciseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("exercises")]
        public IEnumerable<Exercise> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Exercise
            {
                Name = $"Exercise{rng.Next(0xFF)}",
                Index = rng.Next(10)
            }).ToArray();
        }
    }
}
