using System;
using System.Collections.Generic;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.API.Models
{
    public interface IExerciseData
    {
        IEnumerable<IExercise> GetExercises(string name = null);
        IExercise Add(IExercise newExercise);
        IExercise GetById(int id);
        IExercise Update(IExercise updatedExercise);
        IExercise Delete(int id);
        int Commit();
    }
}
