using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IResidenceCommunity
    {
        #region IResidenceCommunity Member

        string[] GetCommunityByCompanyId(object companyId);

        ResidenceCommunityInfo GetModelByJoin(object Id);

        List<ResidenceCommunityInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
