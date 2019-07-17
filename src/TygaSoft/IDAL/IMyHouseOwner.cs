using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IHouseOwner
    {
        #region IHouseOwner Member

        string[] GetHouseOwnerByHouseId(object houseId);

        HouseOwnerInfo GetModelByJoin(object Id);

        List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, string orderBy, params SqlParameter[] cmdParms);

        #endregion
    }
}
