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
    public partial class AdvertisementItem : IAdvertisementItem
    {
        #region IAdvertisementItem Member

        public int Insert(AdvertisementItemInfo model)
        {
            string cmdText = @"insert into AdvertisementItem (AdvertisementId,Descr,ContentText)
			                 values
							 (@AdvertisementId,@Descr,@ContentText)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@AdvertisementId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@Descr",SqlDbType.NVarChar,300),
                                       new SqlParameter("@ContentText",SqlDbType.NVarChar,3000)
                                   };
            parms[0].Value = model.AdvertisementId;
            parms[1].Value = model.Descr;
            parms[2].Value = model.ContentText;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(AdvertisementItemInfo model)
        {
            string cmdText = @"update AdvertisementItem set Descr = @Descr,ContentText = @ContentText 
			                 where AdvertisementId = @AdvertisementId";

            SqlParameter[] parms = {
                                     new SqlParameter("@AdvertisementId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Descr",SqlDbType.NVarChar,300),
                                        new SqlParameter("@ContentText",SqlDbType.NVarChar,3000)
                                   };
            parms[0].Value = model.AdvertisementId;
            parms[1].Value = model.Descr;
            parms[2].Value = model.ContentText;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object AdvertisementId)
        {
            string cmdText = "delete from AdvertisementItem where AdvertisementId = @AdvertisementId";
            SqlParameter parm = new SqlParameter("@AdvertisementId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(AdvertisementId.ToString());

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
                sb.Append(@"delete from AdvertisementItem where AdvertisementId = @AdvertisementId" + n + " ;");
                SqlParameter parm = new SqlParameter("@AdvertisementId" + n + "", SqlDbType.UniqueIdentifier);
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

        public AdvertisementItemInfo GetModel(object AdvertisementId)
        {
            AdvertisementItemInfo model = null;

            string cmdText = @"select top 1 AdvertisementId,Descr,ContentText 
			                   from AdvertisementItem
							   where AdvertisementId = @AdvertisementId ";
            SqlParameter parm = new SqlParameter("@AdvertisementId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(AdvertisementId.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new AdvertisementItemInfo();
                        model.AdvertisementId = reader.GetGuid(0);
                        model.Descr = reader.GetString(1);
                        model.ContentText = reader.GetString(2);
                    }
                }
            }

            return model;
        }

        public IList<AdvertisementItemInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from AdvertisementItem ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return new List<AdvertisementItemInfo>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          AdvertisementId,Descr,ContentText
					  from AdvertisementItem ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementItemInfo> list = new List<AdvertisementItemInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementItemInfo model = new AdvertisementItemInfo();
                        model.AdvertisementId = reader.GetGuid(1);
                        model.Descr = reader.GetString(2);
                        model.ContentText = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementItemInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 AdvertisementId,Descr,ContentText
							 from AdvertisementItem";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementItemInfo> list = new List<AdvertisementItemInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementItemInfo model = new AdvertisementItemInfo();
                        model.AdvertisementId = reader.GetGuid(1);
                        model.Descr = reader.GetString(2);
                        model.ContentText = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementItemInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select AdvertisementId,Descr,ContentText
                              from AdvertisementItem";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            IList<AdvertisementItemInfo> list = new List<AdvertisementItemInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementItemInfo model = new AdvertisementItemInfo();
                        model.AdvertisementId = reader.GetGuid(0);
                        model.Descr = reader.GetString(1);
                        model.ContentText = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementItemInfo> GetList()
        {
            string cmdText = @"select AdvertisementId,Descr,ContentText 
			                from AdvertisementItem
							order by LastUpdatedDate desc ";

            IList<AdvertisementItemInfo> list = new List<AdvertisementItemInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementItemInfo model = new AdvertisementItemInfo();
                        model.AdvertisementId = reader.GetGuid(0);
                        model.Descr = reader.GetString(1);
                        model.ContentText = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
