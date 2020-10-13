using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhysioApp.API.Models;
using PhysioApp.Library.Contracts;

namespace PhysioApp.API.Extensions
{
    public static class Helpers
    {
        public static void DetatchLocal<T>(this AppDbContext context, T t, int entryId) where T : class, IEntity
        {
            var local = context.Set<T>()
                               .Local
                               .FirstOrDefault(x => x.ID.Equals(entryId));

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            context.Entry(t).State = EntityState.Modified;
        }
    }
}
