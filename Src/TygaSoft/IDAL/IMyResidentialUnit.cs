using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IResidentialUnit
    {
        #region IResidentialUnit Member

        string[] GetUnitByBuildingId(object buildingId);

        ResidentialUnitInfo GetModelByJoin(object Id);

        List<ResidentialUnitInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
