﻿using System;
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
            var items = await _appDbContext.WorkoutCatalog
                                           .OrderBy(w => w.Id)
                                           .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> SaveWorkout([FromBody] Workout workout)
        {
            await _appDbContext.WorkoutCatalog.AddAsync(workout);
            await _appDbContext.SaveChangesAsync();


            return Ok(workout);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateWorkout([FromBody] Workout updatedWorkout)
        {
            _appDbContext.WorkoutCatalog.Update(updatedWorkout);
            await _appDbContext.SaveChangesAsync();

            return Ok(updatedWorkout);
        }
    }
}