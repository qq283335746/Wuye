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
    public partial class AdvertisementLink : IAdvertisementLink
    {
        #region IAdvertisementLink Member

        public int Insert(AdvertisementLinkInfo model)
        {
            string cmdText = @"insert into AdvertisementLink (AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable)
			                 values
							 (@AdvertisementId,@ActionTypeId,@ContentPictureId,@Url,@Sort,@IsDisable)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@AdvertisementId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ActionTypeId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ContentPictureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Url",SqlDbType.VarChar,100),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsDisable",SqlDbType.Bit)
                                   };
            parms[0].Value = model.AdvertisementId;
            parms[1].Value = model.ActionTypeId;
            parms[2].Value = model.ContentPictureId;
            parms[3].Value = model.Url;
            parms[4].Value = model.Sort;
            parms[5].Value = model.IsDisable;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(AdvertisementLinkInfo model)
        {
            string cmdText = @"update AdvertisementLink set AdvertisementId = @AdvertisementId,ActionTypeId = @ActionTypeId,ContentPictureId = @ContentPictureId,Url = @Url,Sort = @Sort,IsDisable = @IsDisable 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@AdvertisementId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ActionTypeId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ContentPictureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Url",SqlDbType.VarChar,100),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsDisable",SqlDbType.Bit)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AdvertisementId;
            parms[2].Value = model.ActionTypeId;
            parms[3].Value = model.ContentPictureId;
            parms[4].Value = model.Url;
            parms[5].Value = model.Sort;
            parms[6].Value = model.IsDisable;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from AdvertisementLink where Id = @Id";
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
                sb.Append(@"delete from AdvertisementLink where Id = @Id" + n + " ;");
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

        public AdvertisementLinkInfo GetModel(object Id)
        {
            AdvertisementLinkInfo model = null;

            string cmdText = @"select top 1 Id,AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable 
			                   from AdvertisementLink
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(0);
                        model.AdvertisementId = reader.GetGuid(1);
                        model.ActionTypeId = reader.GetGuid(2);
                        model.ContentPictureId = reader.GetGuid(3);
                        model.Url = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsDisable = reader.GetBoolean(6);
                    }
                }
            }

            return model;
        }

        public IList<AdvertisementLinkInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from AdvertisementLink ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return new List<AdvertisementLinkInfo>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable
					  from AdvertisementLink ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementLinkInfo> list = new List<AdvertisementLinkInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementLinkInfo model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(1);
                        model.AdvertisementId = reader.GetGuid(2);
                        model.ActionTypeId = reader.GetGuid(3);
                        model.ContentPictureId = reader.GetGuid(4);
                        model.Url = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.IsDisable = reader.GetBoolean(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementLinkInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable
							 from AdvertisementLink";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<AdvertisementLinkInfo> list = new List<AdvertisementLinkInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementLinkInfo model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(1);
                        model.AdvertisementId = reader.GetGuid(2);
                        model.ActionTypeId = reader.GetGuid(3);
                        model.ContentPictureId = reader.GetGuid(4);
                        model.Url = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.IsDisable = reader.GetBoolean(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementLinkInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable
                              from AdvertisementLink";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            IList<AdvertisementLinkInfo> list = new List<AdvertisementLinkInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementLinkInfo model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(0);
                        model.AdvertisementId = reader.GetGuid(1);
                        model.ActionTypeId = reader.GetGuid(2);
                        model.ContentPictureId = reader.GetGuid(3);
                        model.Url = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsDisable = reader.GetBoolean(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AdvertisementLinkInfo> GetList()
        {
            string cmdText = @"select Id,AdvertisementId,ActionTypeId,ContentPictureId,Url,Sort,IsDisable 
			                from AdvertisementLink
							order by LastUpdatedDate desc ";

            IList<AdvertisementLinkInfo> list = new List<AdvertisementLinkInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementLinkInfo model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(0);
                        model.AdvertisementId = reader.GetGuid(1);
                        model.ActionTypeId = reader.GetGuid(2);
                        model.ContentPictureId = reader.GetGuid(3);
                        model.Url = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsDisable = reader.GetBoolean(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
