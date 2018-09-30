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
    public partial class ContentPicture : IContentPicture
    {
        #region IContentPicture Member

        public int Insert(ContentPictureInfo model)
        {
            string cmdText = @"insert into ContentPicture (OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate)
			                 values
							 (@OriginalPicture,@BPicture,@MPicture,@SPicture,@OtherPicture,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@OriginalPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@BPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@MPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@SPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@OtherPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OriginalPicture;
            parms[1].Value = model.BPicture;
            parms[2].Value = model.MPicture;
            parms[3].Value = model.SPicture;
            parms[4].Value = model.OtherPicture;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ContentPictureInfo model)
        {
            string cmdText = @"update ContentPicture set OriginalPicture = @OriginalPicture,BPicture = @BPicture,MPicture = @MPicture,SPicture = @SPicture,OtherPicture = @OtherPicture,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OriginalPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@BPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@MPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@SPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@OtherPicture",SqlDbType.VarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.OriginalPicture;
            parms[2].Value = model.BPicture;
            parms[3].Value = model.MPicture;
            parms[4].Value = model.SPicture;
            parms[5].Value = model.OtherPicture;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ContentPicture where Id = @Id";
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
                sb.Append(@"delete from ContentPicture where Id = @Id" + n + " ;");
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

        public ContentPictureInfo GetModel(object Id)
        {
            ContentPictureInfo model = null;

            string cmdText = @"select top 1 Id,OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate 
			                   from ContentPicture
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ContentPictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.OriginalPicture = reader.GetString(1);
                        model.BPicture = reader.GetString(2);
                        model.MPicture = reader.GetString(3);
                        model.SPicture = reader.GetString(4);
                        model.OtherPicture = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<ContentPictureInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ContentPicture ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate
					  from ContentPicture ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<ContentPictureInfo> list = new List<ContentPictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentPictureInfo model = new ContentPictureInfo();
                        model.Id = reader.GetGuid(1);
                        model.OriginalPicture = reader.GetString(2);
                        model.BPicture = reader.GetString(3);
                        model.MPicture = reader.GetString(4);
                        model.SPicture = reader.GetString(5);
                        model.OtherPicture = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentPictureInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate
							 from ContentPicture";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<ContentPictureInfo> list = new List<ContentPictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentPictureInfo model = new ContentPictureInfo();
                        model.Id = reader.GetGuid(1);
                        model.OriginalPicture = reader.GetString(2);
                        model.BPicture = reader.GetString(3);
                        model.MPicture = reader.GetString(4);
                        model.SPicture = reader.GetString(5);
                        model.OtherPicture = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentPictureInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate
                              from ContentPicture";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            IList<ContentPictureInfo> list = new List<ContentPictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentPictureInfo model = new ContentPictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.OriginalPicture = reader.GetString(1);
                        model.BPicture = reader.GetString(2);
                        model.MPicture = reader.GetString(3);
                        model.SPicture = reader.GetString(4);
                        model.OtherPicture = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentPictureInfo> GetList()
        {
            string cmdText = @"select Id,OriginalPicture,BPicture,MPicture,SPicture,OtherPicture,LastUpdatedDate 
			                from ContentPicture
							order by LastUpdatedDate desc ";

            IList<ContentPictureInfo> list = new List<ContentPictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentPictureInfo model = new ContentPictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.OriginalPicture = reader.GetString(1);
                        model.BPicture = reader.GetString(2);
                        model.MPicture = reader.GetString(3);
                        model.SPicture = reader.GetString(4);
                        model.OtherPicture = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
