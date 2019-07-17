using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IAdvertisement
    {
        #region IAdvertisement Member

        Guid InsertByOutput(AdvertisementInfo model);

        bool DeleteBatchByJoin(IList<object> list);

        AdvertisementInfo GetModelByJoin(object Id);

        DataSet GetDs(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<AdvertisementInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
