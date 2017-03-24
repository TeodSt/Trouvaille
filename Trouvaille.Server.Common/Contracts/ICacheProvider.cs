using System.Web.Caching;

namespace Trouvaille.Server.Common.Contracts
{
    public interface ICacheProvider
    {
        void InsertWithSqlDependency(string key, object value, CacheDependency dependency);

        object GetValueOfCache(string key);
    }
}
