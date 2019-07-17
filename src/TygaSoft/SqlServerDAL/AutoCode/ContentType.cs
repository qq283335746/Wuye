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
    public partial class ContentType : IContentType
    {
        #region IContentType Member

        public int Insert(ContentTypeInfo model)
        {
            string cmdText = @"insert into ContentType (TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate)
			                 values
							 (@TypeName,@TypeCode,@TypeValue,@ParentId,@Sort,@IsSys,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@TypeName",SqlDbType.NVarChar,50),
new SqlParameter("@TypeCode",SqlDbType.VarChar,50),
new SqlParameter("@TypeValue",SqlDbType.NVarChar,256),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsSys",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.TypeName;
            parms[1].Value = model.TypeCode;
            parms[2].Value = model.TypeValue;
            parms[3].Value = model.ParentId;
            parms[4].Value = model.Sort;
            parms[5].Value = model.IsSys;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ContentTypeInfo model)
        {
            string cmdText = @"update ContentType set TypeName = @TypeName,TypeCode = @TypeCode,TypeValue = @TypeValue,ParentId = @ParentId,Sort = @Sort,IsSys = @IsSys,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@TypeName",SqlDbType.NVarChar,50),
new SqlParameter("@TypeCode",SqlDbType.VarChar,50),
new SqlParameter("@TypeValue",SqlDbType.NVarChar,256),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsSys",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.TypeName;
            parms[2].Value = model.TypeCode;
            parms[3].Value = model.TypeValue;
            parms[4].Value = model.ParentId;
            parms[5].Value = model.Sort;
            parms[6].Value = model.IsSys;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ContentType where Id = @Id";
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
                sb.Append(@"delete from ContentType where Id = @Id" + n + " ;");
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

        public ContentTypeInfo GetModel(object Id)
        {
            ContentTypeInfo model = null;

            string cmdText = @"select top 1 Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate 
			                   from ContentType
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(0);
                        model.TypeName = reader.GetString(1);
                        model.TypeCode = reader.GetString(2);
                        model.TypeValue = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsSys = reader.GetBoolean(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ContentType ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return new List<ContentTypeInfo>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc,Sort) as RowNumber,
			          Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate
					  from ContentType ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);
                        model.TypeCode = reader.GetString(3);
                        model.TypeValue = reader.GetString(4);
                        model.ParentId = reader.GetGuid(5);
                        model.Sort = reader.GetInt32(6);
                        model.IsSys = reader.GetBoolean(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate
							 from ContentType";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);
                        model.TypeCode = reader.GetString(3);
                        model.TypeValue = reader.GetString(4);
                        model.ParentId = reader.GetGuid(5);
                        model.Sort = reader.GetInt32(6);
                        model.IsSys = reader.GetBoolean(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ContentTypeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate
                              from ContentType";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(0);
                        model.TypeName = reader.GetString(1);
                        model.TypeCode = reader.GetString(2);
                        model.TypeValue = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsSys = reader.GetBoolean(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ContentTypeInfo> GetList()
        {
            string cmdText = @"select Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate 
			                from ContentType
							order by LastUpdatedDate desc ";

            List<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(0);
                        model.TypeName = reader.GetString(1);
                        model.TypeCode = reader.GetString(2);
                        model.TypeValue = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsSys = reader.GetBoolean(6);
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
