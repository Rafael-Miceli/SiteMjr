using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteMjr.Models
{
    public class CacheService : ICacheService
    {
        public T Get<T>(string cacheId, Func<T> getItemCallback) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;

            if (item != null) return item;

            item = getItemCallback();

            if (item == null) return null;

            HttpContext.Current.Cache.Insert(cacheId, item);
            return item;
        }

        public void Remove(string cacheId)
        {
            HttpRuntime.Cache.Remove(cacheId);
        }
    }

    public interface ICacheService
    {
        T Get<T>(string cacheId, Func<T> getItemCallback) where T : class;

        void Remove(string cacheId);
    }
}