using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class ComplainRepair
    {
        public IList<ComplainRepairInfo> GetListByUser(int pageIndex,int pageSize, out int totalRecords, object userId, string enumCode)
        {
            string sqlWhere = "and cr.UserId = @UserId and se.EnumCode = @EnumCode ";
            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@EnumCode",enumCode)
                                   };
            parms[0].Value = Guid.Parse(userId.ToString());
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, parms);
        }

        public bool DealStatusBatch(Dictionary<Guid, int> dic)
        {
            return dal.DealStatusBatch(dic);
        }

        public IList<ComplainRepairInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }
    }
}
