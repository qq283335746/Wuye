using System.Configuration;
using System.Web.Caching;
using System.Collections.Generic;
using TygaSoft.ICacheDependency;

namespace TygaSoft.CacheDependencyFactory
{
    public static class DependencyFacade
    {
        private static readonly string path = ConfigurationManager.AppSettings["CacheDependencyAssembly"];

        public static AggregateCacheDependency GetSysEnumDependency()
        {
            if (!string.IsNullOrEmpty(path))
                return DependencyAccess.CreateSysEnumDependency().GetDependency();
            else
                return null;
        }
    }
}
