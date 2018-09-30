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
    public partial class HouseOwnerNotice : IHouseOwnerNotice
    {
        #region 成员方法

        public int Insert(HouseOwnerNoticeInfo model)
        {
            string cmdText = @"insert into HouseOwnerNotice (HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate)
			                 values
							 (@HouseOwnerId,@NoticeId,@Status,@IsRead,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@NoticeId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Status",SqlDbType.TinyInt),
                                        new SqlParameter("@IsRead",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.HouseOwnerId;
            parms[1].Value = model.NoticeId;
            parms[2].Value = model.Status;
            parms[3].Value = model.IsRead;
            parms[4].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(HouseOwnerNoticeInfo model)
        {
            string cmdText = @"update HouseOwnerNotice set HouseOwnerId = @HouseOwnerId,NoticeId = @NoticeId,Status = @Status,IsRead = @IsRead,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@NoticeId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Status",SqlDbType.TinyInt),
                                        new SqlParameter("@IsRead",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.HouseOwnerId;
            parms[2].Value = model.NoticeId;
            parms[3].Value = model.Status;
            parms[4].Value = model.IsRead;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from HouseOwnerNotice where Id = @Id";
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
                sb.Append(@"delete from HouseOwnerNotice where Id = @Id" + n + " ;");
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

        public HouseOwnerNoticeInfo GetModel(object Id)
        {
            HouseOwnerNoticeInfo model = null;

            string cmdText = @"select top 1 Id,HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate 
			                   from HouseOwnerNotice
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.NoticeId = reader.GetGuid(2);
                        model.Status = reader.GetByte(3);
                        model.IsRead = reader.GetBoolean(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);
                    }
                }
            }

            return model;
        }

        public List<HouseOwnerNoticeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from HouseOwnerNotice ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate
					  from HouseOwnerNotice ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseOwnerNoticeInfo> list = new List<HouseOwnerNoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerNoticeInfo model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseOwnerId = reader.GetGuid(2);
                        model.NoticeId = reader.GetGuid(3);
                        model.Status = reader.GetByte(4);
                        model.IsRead = reader.GetBoolean(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerNoticeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate
							 from HouseOwnerNotice";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseOwnerNoticeInfo> list = new List<HouseOwnerNoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerNoticeInfo model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseOwnerId = reader.GetGuid(2);
                        model.NoticeId = reader.GetGuid(3);
                        model.Status = reader.GetByte(4);
                        model.IsRead = reader.GetBoolean(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerNoticeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate
                              from HouseOwnerNotice";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<HouseOwnerNoticeInfo> list = new List<HouseOwnerNoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerNoticeInfo model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.NoticeId = reader.GetGuid(2);
                        model.Status = reader.GetByte(3);
                        model.IsRead = reader.GetBoolean(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerNoticeInfo> GetList()
        {
            string cmdText = @"select Id,HouseOwnerId,NoticeId,Status,IsRead,LastUpdatedDate 
			                from HouseOwnerNotice
							order by LastUpdatedDate desc ";

            List<HouseOwnerNoticeInfo> list = new List<HouseOwnerNoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerNoticeInfo model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.NoticeId = reader.GetGuid(2);
                        model.Status = reader.GetByte(3);
                        model.IsRead = reader.GetBoolean(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
