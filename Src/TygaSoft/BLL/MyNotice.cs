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
    public partial class Notice
    {
        public NoticeInfo GetMyNoticeModel(Guid Id,object userId)
        {
            string sqlWhere = "and uho.UserId = @UserId and n.Id = @Id ";
            SqlParameter[] parms = {
                                       new SqlParameter("@Id", SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = Id;
            parms[1].Value = Guid.Parse(userId.ToString());

            return dal.GetModelByJoin(sqlWhere, parms);
        }

        public IList<NoticeInfo> GetMyNotice(int pageIndex, int pageSize, out int totalRecords, object userId)
        { 
            string sqlWhere = "and uho.UserId = @UserId ";
            SqlParameter parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(userId.ToString());

            return dal.GetListByJoin(pageIndex, pageSize,out totalRecords, sqlWhere, parm);
        }

        public IList<NoticeInfo> GetListExceptContent(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListExceptContent(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }
    }
}
