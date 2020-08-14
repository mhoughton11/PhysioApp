using System.Collections.Generic;
using System.Linq;
using DIYHIIT.Library.Contracts;
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

        public IExercise Add(IExercise newExercise)
        {
            context.Add(newExercise);
            return newExercise;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public IExercise Delete(int id)
        {
            var ex = context.Exercises.SingleOrDefault(e => e.ID == id);
            if (ex != null)
            {
                context.Remove(ex);
            }
            return ex;
        }

        public IExercise GetById(int id)
        {
            return context.Exercises.Find(id);
        }

        public IEnumerable<IExercise> GetExercises(string name = null)
        {
            var query = from e in context.Exercises
                        where e.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby e.Name
                        select e;

            return query;
        }

        public IExercise Update(IExercise updatedExercise)
        {
            var entity = context.Exercises.Attach(updatedExercise);
            entity.State = EntityState.Modified;
            return updatedExercise;
        }
    }
}
