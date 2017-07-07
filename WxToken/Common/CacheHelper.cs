using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace WxToken.Common
{
    public static class CacheHelper
    {
       private static System.Web.Caching.Cache  cache = HttpRuntime.Cache;
        public static void Insert()
        {

        }
        public static void Add(string key,object value, TimeSpan cacheDuration)
        {
           cache.Add(key, value, null, DateTime.Now.Add(cacheDuration), cacheDuration, CacheItemPriority.NotRemovable, null);
        }
        public static object Get(string key)
        {
            return cache.Get(key);
        }
    }
}