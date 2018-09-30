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
    public partial class Advertisement : IAdvertisement
    {
        #region IAdvertisement Member

        public int Insert(AdvertisementInfo model)
        {
            string cmdText = @"insert into Advertisement (Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate)
			                 values
							 (@Id,@Title,@SiteFunId,@LayoutPositionId,@Timeout,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                        new SqlParameter("@SiteFunId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@LayoutPositionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Timeout",SqlDbType.Int),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = Guid.Parse(model.Id.ToString());
            parms[1].Value = model.Title;
            parms[2].Value = model.SiteFunId;
            parms[3].Value = model.LayoutPositionId;
            parms[4].Value = model.Timeout;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(AdvertisementInfo model)
        {
            string cmdText = @"update Advertisement set Title = @Title,SiteFunId = @SiteFunId,LayoutPositionId = @LayoutPositionId,Timeout = @Timeout,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                    new SqlParameter("@SiteFunId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@LayoutPositionId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@Timeout",SqlDbType.Int),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Title;
            parms[2].Value = model.SiteFunId;
            parms[3].Value = model.LayoutPositionId;
            parms[4].Value = model.Timeout;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from Advertisement where Id = @Id";
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
            foreach (var item in list)
            {
                n++;
                sb.Append(@"delete from Advertisement where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item.ToString());
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

        public AdvertisementInfo GetModel(object Id)
        {
            AdvertisementInfo model = null;

            string cmdText = @"select top 1 Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate 
			                   from Advertisement
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new AdvertisementInfo();
                        model.Id = reader.GetGuid(0);
                        model.Title = reader.GetString(1);
                        model.SiteFunId = reader.GetGuid(2);
                        model.LayoutPositionId = reader.GetGuid(3);
                        model.Timeout = reader.GetInt32(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);
                    }
                }
            }

            return model;
        }

        public IList<AdvertisementInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from Advertisement ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return new List<AdvertisementInfo>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate
					  from Advertisement ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementInfo model = new AdvertisementInfo();
                        model.Id = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.SiteFunId = reader.GetGuid(3);
                        model.LayoutPositionId = reader.GetGuid(4);
                        model.Timeout = reader.GetInt32(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate
							 from Advertisement";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementInfo model = new AdvertisementInfo();
                        model.Id = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.SiteFunId = reader.GetGuid(3);
                        model.LayoutPositionId = reader.GetGuid(4);
                        model.Timeout = reader.GetInt32(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate
                              from Advertisement";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementInfo model = new AdvertisementInfo();
                        model.Id = reader.GetGuid(0);
                        model.Title = reader.GetString(1);
                        model.SiteFunId = reader.GetGuid(2);
                        model.LayoutPositionId = reader.GetGuid(3);
                        model.Timeout = reader.GetInt32(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementInfo> GetList()
        {
            string cmdText = @"select Id,Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate 
			                from Advertisement
							order by LastUpdatedDate desc ";

            IList<AdvertisementInfo> list = new List<AdvertisementInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementInfo model = new AdvertisementInfo();
                        model.Id = reader.GetGuid(0);
                        model.Title = reader.GetString(1);
                        model.SiteFunId = reader.GetGuid(2);
                        model.LayoutPositionId = reader.GetGuid(3);
                        model.Timeout = reader.GetInt32(4);
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
