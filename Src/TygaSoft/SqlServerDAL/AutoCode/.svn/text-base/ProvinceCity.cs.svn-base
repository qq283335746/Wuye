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
    public partial class ProvinceCity : IProvinceCity
    {
        #region 成员方法

        public int Insert(ProvinceCityInfo model)
        {
            string cmdText = @"insert into ProvinceCity (Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate)
			                 values
							 (@Named,@Pinyin,@FirstChar,@ParentId,@Sort,@Remark,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@Named",SqlDbType.NVarChar,30),
                                        new SqlParameter("@Pinyin",SqlDbType.VarChar,30),
                                        new SqlParameter("@FirstChar",SqlDbType.Char,1),
                                        new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Named;
            parms[1].Value = model.Pinyin;
            parms[2].Value = model.FirstChar;
            parms[3].Value = model.ParentId;
            parms[4].Value = model.Sort;
            parms[5].Value = model.Remark;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ProvinceCityInfo model)
        {
            string cmdText = @"update ProvinceCity set Named = @Named,Pinyin = @Pinyin,FirstChar = @FirstChar,ParentId = @ParentId,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,30),
                                        new SqlParameter("@Pinyin",SqlDbType.VarChar,30),
                                        new SqlParameter("@FirstChar",SqlDbType.Char,1),
                                        new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Named;
            parms[2].Value = model.Pinyin;
            parms[3].Value = model.FirstChar;
            parms[4].Value = model.ParentId;
            parms[5].Value = model.Sort;
            parms[6].Value = model.Remark;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ProvinceCity where Id = @Id";
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
                sb.Append(@"delete from ProvinceCity where Id = @Id" + n + " ;");
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

        public ProvinceCityInfo GetModel(object Id)
        {
            ProvinceCityInfo model = null;

            string cmdText = @"select top 1 Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate 
			                   from ProvinceCity
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.Pinyin = reader.GetString(2);
                        model.FirstChar = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public List<ProvinceCityInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ProvinceCity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate
					  from ProvinceCity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ProvinceCityInfo> list = new List<ProvinceCityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProvinceCityInfo model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.Pinyin = reader.GetString(3);
                        model.FirstChar = reader.GetString(4);
                        model.ParentId = reader.GetGuid(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ProvinceCityInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			                 Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate
							 from ProvinceCity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ProvinceCityInfo> list = new List<ProvinceCityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProvinceCityInfo model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.Pinyin = reader.GetString(3);
                        model.FirstChar = reader.GetString(4);
                        model.ParentId = reader.GetGuid(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ProvinceCityInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate
                              from ProvinceCity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;

            List<ProvinceCityInfo> list = new List<ProvinceCityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProvinceCityInfo model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.Pinyin = reader.GetString(2);
                        model.FirstChar = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ProvinceCityInfo> GetList()
        {
            string cmdText = @"select Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate 
			                from ProvinceCity
							order by LastUpdatedDate ";

            List<ProvinceCityInfo> list = new List<ProvinceCityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProvinceCityInfo model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.Pinyin = reader.GetString(2);
                        model.FirstChar = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.Remark = reader.GetString(6);
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
