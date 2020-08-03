using System;
using System.Collections.Generic;
using DIYHIIT.Models.Exercise;

namespace DIYHIIT.API.Models
{
    public interface IExerciseData
    {
        IEnumerable<Exercise> GetExercises(string name = null);
        Exercise Add(Exercise newExercise);
        Exercise GetById(int id);
        Exercise Update(Exercise updatedExercise);
        Exercise Delete(int id);
        int Commit();
    }
}
