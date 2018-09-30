using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class SysEnumHelper
    {
        #region 投诉保修相关

        public static string GetEnumNameForComplainRepairType(object value)
        {
            return Enum.GetName(typeof(ComplainRepairType), value);
        }

        public static string GetEnumNameForComplainRepairStatus(object value)
        {
            return Enum.GetName(typeof(ComplainRepairStatus), value);
        }

        public enum ComplainRepairType
        {
            PublicTerritory, ResidentFamily
        }

        public enum ComplainRepairStatus:byte
        {
            受理中, 已受理
        }

        #endregion

        #region 广告相关

        public enum ContentType
        {
            AdvertisementFun
        }

        #endregion
    }
}
