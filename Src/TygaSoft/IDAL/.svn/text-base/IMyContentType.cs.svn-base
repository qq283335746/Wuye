using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IContentType
    {
        /// <summary>
        /// 获取满足当前条件的键值对数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        Dictionary<object, string> GetKeyValue(string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        ContentTypeInfo GetModelByTypeCode(string typeCode);
    }
}
