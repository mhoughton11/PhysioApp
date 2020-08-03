using System.Collections.Generic;
using System.Linq;
using DIYHIIT.Models.Exercise;
using Microsoft.EntityFrameworkCore;

namespace DIYHIIT.API.Models
{
    public class SqlExericseData : IExerciseData
    {
        private readonly AppDbContext context;

        public SqlExericseData(AppDbContext context)
        {
            this.context = context;
        }

        public Exercise Add(Exercise newExercise)
        {
            context.Add(newExercise);
            return newExercise;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public Exercise Delete(int id)
        {
            var ex = context.Exercises.SingleOrDefault(e => e.ID == id);
            if (ex != null)
            {
                context.Remove(ex);
            }
            return ex;
        }

        public Exercise GetById(int id)
        {
            return context.Exercises.Find(id);
        }

        public IEnumerable<Exercise> GetExercises(string name = null)
        {
            var query = from e in context.Exercises
                        where e.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby e.Name
                        select e;

            return query;
        }

        public Exercise Update(Exercise updatedExercise)
        {
            var entity = context.Exercises.Attach(updatedExercise);
            entity.State = EntityState.Modified;
            return updatedExercise;
        }
    }
}
