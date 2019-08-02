using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TygaSoft.SysHelper
{
    public class ConfigHelper
    {
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public const string AspnetDbDbo = "TygaSoftAspnetDb.dbo.";
        public const string WuyeDbDbo = "WuyeDb.dbo.";
    }
}
