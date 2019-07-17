using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;


namespace TygaSoft.SqlServerDAL
{
    public partial class ComplainRepair : IComplainRepair
    {
        public bool DealStatusBatch(Dictionary<Guid, int> dic)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder();
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (KeyValuePair<Guid, int> kvp in dic)
            {
                n++;
                sb.AppendFormat(@"update ComplainRepair set Status = @Status" + n + " where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = kvp.Key;
                parms.Add(parm);
                parm = new SqlParameter("@Status" + n + "", SqlDbType.TinyInt);
                parm.Value = kvp.Value;
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.SqlProviderConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public IList<ComplainRepairInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) 
                               from ComplainRepair cr
                               left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = cr.UserId 
                               left join Sys_Enum se on se.Id = cr.SysEnumId 
                             ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return null;

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by cr.LastUpdatedDate desc) as RowNumber,
			          cr.Id,cr.UserId,cr.SysEnumId,cr.Phone,cr.Address,cr.Descri,cr.Status,cr.LastUpdatedDate,
                      se.EnumValue, u.UserName
					  from ComplainRepair cr
                      left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = cr.UserId 
                      left join Sys_Enum se on se.Id = cr.SysEnumId 
                      ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<ComplainRepairInfo> list = new List<ComplainRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ComplainRepairInfo model = new ComplainRepairInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.SysEnumId = reader.GetGuid(3);
                        model.Phone = reader.GetString(4);
                        model.Address = reader.GetString(5);
                        model.Descri = reader.GetString(6);
                        model.Status = reader.GetByte(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                        model.StatusName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.UserName = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
