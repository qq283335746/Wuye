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
    public partial class Advertisement
    {
        #region IAdvertisement Member

        public Guid InsertByOutput(AdvertisementInfo model)
        {
            string cmdText = @"insert into Advertisement (Title,SiteFunId,LayoutPositionId,Timeout,LastUpdatedDate)
			                 output inserted.Id values
							 (@Title,@SiteFunId,@LayoutPositionId,@Timeout,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@Title",SqlDbType.NVarChar,100),
                                        new SqlParameter("@SiteFunId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@LayoutPositionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Timeout",SqlDbType.Int),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };

            parms[0].Value = model.Title;
            parms[1].Value = model.SiteFunId;
            parms[2].Value = model.LayoutPositionId;
            parms[3].Value = model.Timeout;
            parms[4].Value = model.LastUpdatedDate;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return Guid.Parse(obj.ToString());

            return Guid.Empty;
        }

        public bool DeleteBatchByJoin(IList<object> list)
        {
            if (list == null || list.Count == 0) return false;

            bool result = false;
            StringBuilder sb = new StringBuilder();
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Advertisement where Id = @Id" + n + " ;");
                sb.Append(@"delete from AdvertisementItem where AdvertisementId = @Id" + n + " ;");
                sb.Append(@"delete from AdvertisementLink where AdvertisementId = @Id" + n + " ;");
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

        public AdvertisementInfo GetModelByJoin(object Id)
        {
            AdvertisementInfo model = null;

            string cmdText = @"select top 1 ad.Id,ad.Title,ad.SiteFunId,ad.LayoutPositionId,ad.Timeout,ad.LastUpdatedDate,
                               at.TypeCode as SiteFunName,at1.TypeCode as LayoutPositionName,adi.Descr,adi.ContentText
                               from Advertisement ad
                               left join ContentType at on at.Id = ad.SiteFunId
                               left join ContentType at1 on at1.Id = ad.LayoutPositionId
                               left join AdvertisementItem adi on adi.AdvertisementId = ad.Id
							   where ad.Id = @Id ";
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
                        model.SiteFunName = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        model.LayoutPositionName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.Descr = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.ContentText = reader.IsDBNull(9) ? "" : reader.GetString(9);

                    }
                }
            }

            return model;
        }

        public DataSet GetDs(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(1000);
            sb.Append(@"select count(*) 
                        from Advertisement ad
                        left join ContentType at on at.Id = ad.SiteFunId
                        left join ContentType at1 on at1.Id = ad.LayoutPositionId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return null;

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ad.LastUpdatedDate desc) as RowNumber,
			          ad.Id,ad.Title,ad.SiteFunId,ad.LayoutPositionId,ad.Timeout,ad.LastUpdatedDate,
                      at.TypeCode as SiteFunCode,at.TypeName as SiteFunName,at.TypeValue as SiteFunValue,at1.TypeCode as LayoutPositionCode,at1.TypeName as LayoutPositionName,at1.TypeValue as LayoutPositionValue
					  from Advertisement ad
                        left join ContentType at on at.Id = ad.SiteFunId
                        left join ContentType at1 on at1.Id = ad.LayoutPositionId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            return SqlHelper.ExecuteDataset(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public IList<AdvertisementInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(1) 
                             from Advertisement ad
                              left join ContentType at on at.Id = ad.SiteFunId
                              left join ContentType at1 on at1.Id = ad.LayoutPositionId
                             ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return new List<AdvertisementInfo>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by ad.LastUpdatedDate desc) as RowNumber,
			          ad.Id,ad.Title,ad.SiteFunId,ad.LayoutPositionId,ad.Timeout,ad.LastUpdatedDate,
                      at.TypeCode as SiteFunName,at1.TypeCode as LayoutPositionName
					  from Advertisement ad
                        left join ContentType at on at.Id = ad.SiteFunId
                        left join ContentType at1 on at1.Id = ad.LayoutPositionId
                      ";
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
                        model.SiteFunName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.LayoutPositionName = reader.IsDBNull(8) ? "" : reader.GetString(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
