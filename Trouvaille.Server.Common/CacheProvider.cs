using System;
using System.Web;
using System.Web.Caching;
using Trouvaille.Server.Common.Contracts;

namespace Trouvaille.Server.Common
{
    public class CacheProvider : ICacheProvider
    {
        private readonly HttpContextBase context;

        public CacheProvider(HttpContextBase context)
        {
            this.context = context;
        }

        public object GetValueOfCache(string key)
        {
            return this.context.Cache[key];
        }

        public CacheDependency SqlCacheDependency(string databaseEntryName, string tableName)
        {
            return new SqlCacheDependency(databaseEntryName, tableName);
        }

        public void InsertWithSqlDependency(string key, object value, CacheDependency dependency)
        {
            this.context.Cache.Insert(
                key,   // key
                value, // value
                dependency,                                         // dependencies
                DateTime.Now.AddDays(5),                            // absolute exp.
                TimeSpan.Zero,                                      // sliding exp.
                CacheItemPriority.Default,                          // priority
                null);                                              // callback delegate            
        }
    }
}
