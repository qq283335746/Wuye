using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IAdvertisementLink
    {
        #region IAdvertisementLink Member

        int DeleteByAdId(object advertisementId);

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="dt"></param>
        void InsertBatch(DataTable dt);

        DataSet GetDs(string sqlWhere, params SqlParameter[] cmdParms);

        IList<AdvertisementLinkInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
