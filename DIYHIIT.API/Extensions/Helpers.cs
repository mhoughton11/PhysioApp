using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DIYHIIT.API.Models;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.API.Extensions
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
