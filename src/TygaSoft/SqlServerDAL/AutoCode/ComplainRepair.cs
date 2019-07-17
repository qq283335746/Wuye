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
        #region 成员方法

        public int Insert(ComplainRepairInfo model)
        {
            string cmdText = @"insert into ComplainRepair (UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate)
			                 values
							 (@UserId,@SysEnumId,@Phone,@Address,@Descri,@Status,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@SysEnumId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                        new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Descri",SqlDbType.NVarChar,1000),
                                        new SqlParameter("@Status",SqlDbType.TinyInt),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.SysEnumId;
            parms[2].Value = model.Phone;
            parms[3].Value = model.Address;
            parms[4].Value = model.Descri;
            parms[5].Value = model.Status;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ComplainRepairInfo model)
        {
            string cmdText = @"update ComplainRepair set UserId = @UserId,SysEnumId = @SysEnumId,Phone = @Phone,Address = @Address,Descri = @Descri,Status = @Status,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@SysEnumId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Phone",SqlDbType.VarChar,20),
                                        new SqlParameter("@Address",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Descri",SqlDbType.NVarChar,1000),
                                        new SqlParameter("@Status",SqlDbType.TinyInt),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.SysEnumId;
            parms[3].Value = model.Phone;
            parms[4].Value = model.Address;
            parms[5].Value = model.Descri;
            parms[6].Value = model.Status;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ComplainRepair where Id = @Id";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            if (list == null || list.Count == 0) return false;

            bool result = false;
            StringBuilder sb = new StringBuilder();
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from ComplainRepair where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
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

        public ComplainRepairInfo GetModel(object Id)
        {
            ComplainRepairInfo model = null;

            string cmdText = @"select top 1 Id,UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate 
			                   from ComplainRepair
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ComplainRepairInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SysEnumId = reader.GetGuid(2);
                        model.Phone = reader.GetString(3);
                        model.Address = reader.GetString(4);
                        model.Descri = reader.GetString(5);
                        model.Status = reader.GetByte(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public List<ComplainRepairInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ComplainRepair ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate
					  from ComplainRepair ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ComplainRepairInfo> list = new List<ComplainRepairInfo>();

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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ComplainRepairInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate
							 from ComplainRepair";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ComplainRepairInfo> list = new List<ComplainRepairInfo>();

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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ComplainRepairInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate
                              from ComplainRepair";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<ComplainRepairInfo> list = new List<ComplainRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ComplainRepairInfo model = new ComplainRepairInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SysEnumId = reader.GetGuid(2);
                        model.Phone = reader.GetString(3);
                        model.Address = reader.GetString(4);
                        model.Descri = reader.GetString(5);
                        model.Status = reader.GetByte(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ComplainRepairInfo> GetList()
        {
            string cmdText = @"select Id,UserId,SysEnumId,Phone,Address,Descri,Status,LastUpdatedDate 
			                from ComplainRepair
							order by LastUpdatedDate desc ";

            List<ComplainRepairInfo> list = new List<ComplainRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ComplainRepairInfo model = new ComplainRepairInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SysEnumId = reader.GetGuid(2);
                        model.Phone = reader.GetString(3);
                        model.Address = reader.GetString(4);
                        model.Descri = reader.GetString(5);
                        model.Status = reader.GetByte(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
