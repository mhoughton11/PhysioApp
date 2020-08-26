using Akavache;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Linq;
using System;
using DIYHIIT.Library.Contracts;

namespace DIYHIIT.Services.Data
{
    public class BaseService 
    {
        protected IBlobCache Cache;

        public BaseService(IBlobCache cache)
        {
            Cache = cache ?? BlobCache.LocalMachine;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public async Task<T> GetFromCache<T>(int id) where T : IBaseModel
        {
            try
            {
                var all = await Cache.GetAllObjects<T>();
                return all.Where(t => t.Id == id).SingleOrDefault();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public void InvalidateCache<T>()
        {
            Cache.InvalidateAllObjects<T>();
        }
    }
}
