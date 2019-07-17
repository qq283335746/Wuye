using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IResidentialBuilding
    {
        #region Member

        string[] GetBuildingByCommunityId(object communityId);

        ResidentialBuildingInfo GetModelByJoin(object Id);

        List<ResidentialBuildingInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
