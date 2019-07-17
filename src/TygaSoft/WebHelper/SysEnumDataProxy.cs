using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.CacheDependencyFactory;

namespace TygaSoft.WebHelper
{
    public class SysEnumDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int sysEnumTimeout = int.Parse(ConfigurationManager.AppSettings["SysEnumCacheDuration"]);

        /// <summary>
        /// 获取包含当前枚举代号下的所有子节点项数据列表
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public static List<SysEnumInfo> GetListIncludeChild(string enumCode)
        {
            SysEnum bll = new SysEnum();

            if (!enableCaching)
            {
                return bll.GetListIncludeChild(enumCode);
            }

            string key = "SysEnum_GetListIncludeChild_" + enumCode + "";
            List<SysEnumInfo> data = (List<SysEnumInfo>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetListIncludeChild(enumCode);

                AggregateCacheDependency cd = DependencyFacade.GetSysEnumDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(sysEnumTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        /// <summary>
        /// 获取属于当前枚举代号的所有节点项json格式字符串
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public static string GetJsonForEnumCode(string enumCode)
        {
            SysEnum bll = new SysEnum();

            if (!enableCaching)
            {
                return bll.GetTreeJsonForEnumCode("DicCode");
            }

            string key = "SysEnum_Json_" + enumCode + "";
            string data = (string)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetTreeJsonForEnumCode("DicCode");

                AggregateCacheDependency cd = DependencyFacade.GetSysEnumDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(sysEnumTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        public static List<SysEnumInfo> GetList()
        {
            SysEnum bll = new SysEnum();

            if (!enableCaching)
            {
                return bll.GetList();
            }

            string key = "sysEnum_list";
            List<SysEnumInfo> data = (List<SysEnumInfo>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList();

                AggregateCacheDependency cd = DependencyFacade.GetSysEnumDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(sysEnumTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        public static List<SysEnumInfo> GetList(string parentName)
        {
            SysEnum bll = new SysEnum();

            SqlParameter parm = new SqlParameter("@EnumName", parentName);

            if (!enableCaching)
            {
                return bll.GetList(1, 100000, "and t2.EnumName = @EnumName",parm);
            }

            string key = "sysEnum_list_" + parentName + "";
            List<SysEnumInfo> data = (List<SysEnumInfo>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 100000, "and t2.EnumName = @EnumName", parm);

                AggregateCacheDependency cd = DependencyFacade.GetSysEnumDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(sysEnumTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        public static List<SysEnumInfo> GetListByCode(string parentCode)
        {
            SysEnum bll = new SysEnum();

            SqlParameter parm = new SqlParameter("@EnumCode", parentCode);

            if (!enableCaching)
            {
                return bll.GetList(1, 100000, "and t2.EnumCode = @EnumCode", parm);
            }

            string key = "sysEnum_list_" + parentCode + "";
            List<SysEnumInfo> data = (List<SysEnumInfo>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetList(1, 100000, "and t2.EnumCode = @EnumCode", parm);

                AggregateCacheDependency cd = DependencyFacade.GetSysEnumDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(sysEnumTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
