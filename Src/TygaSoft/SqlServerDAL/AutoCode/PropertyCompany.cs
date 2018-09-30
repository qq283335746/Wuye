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
    public partial class PropertyCompany : IPropertyCompany
    {
        #region 成员方法

        public int Insert(PropertyCompanyInfo model)
        {
            string cmdText = @"insert into PropertyCompany (CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate)
			                 values
							 (@CompanyName,@ShortName,@Province,@ProvinceCityId,@City,@District,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@CompanyName",SqlDbType.NVarChar,30),
                                        new SqlParameter("@ShortName",SqlDbType.VarChar,30),
                                        new SqlParameter("@Province",SqlDbType.NVarChar,10),
                                        new SqlParameter("@ProvinceCityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@City",SqlDbType.NVarChar,10),
                                        new SqlParameter("@District",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.CompanyName;
            parms[1].Value = model.ShortName;
            parms[2].Value = model.Province;
            parms[3].Value = model.ProvinceCityId;
            parms[4].Value = model.City;
            parms[5].Value = model.District;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(PropertyCompanyInfo model)
        {
            string cmdText = @"update PropertyCompany set CompanyName = @CompanyName,ShortName = @ShortName,Province = @Province,ProvinceCityId = @ProvinceCityId,City = @City,District = @District,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                        new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CompanyName",SqlDbType.NVarChar,30),
                                        new SqlParameter("@ShortName",SqlDbType.VarChar,30),
                                        new SqlParameter("@Province",SqlDbType.NVarChar,10),
                                        new SqlParameter("@ProvinceCityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@City",SqlDbType.NVarChar,10),
                                        new SqlParameter("@District",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CompanyName;
            parms[2].Value = model.ShortName;
            parms[3].Value = model.Province;
            parms[4].Value = model.ProvinceCityId;
            parms[5].Value = model.City;
            parms[6].Value = model.District;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from PropertyCompany where Id = @Id";
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
                sb.Append(@"delete from PropertyCompany where Id = @Id" + n + " ;");
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

        public PropertyCompanyInfo GetModel(object Id)
        {
            PropertyCompanyInfo model = null;

            string cmdText = @"select top 1 Id,CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate 
			                   from PropertyCompany
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new PropertyCompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyName = reader.GetString(1);
                        model.ShortName = reader.GetString(2);
                        model.Province = reader.GetString(3);
                        model.ProvinceCityId = reader.GetGuid(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public List<PropertyCompanyInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from PropertyCompany ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          Id,CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate
					  from PropertyCompany ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<PropertyCompanyInfo> list = new List<PropertyCompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PropertyCompanyInfo model = new PropertyCompanyInfo();
                        model.Id = reader.GetGuid(1);
                        model.CompanyName = reader.GetString(2);
                        model.ShortName = reader.GetString(3);
                        model.Province = reader.GetString(4);
                        model.ProvinceCityId = reader.GetGuid(5);
                        model.City = reader.GetString(6);
                        model.District = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<PropertyCompanyInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			                 Id,CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate
							 from PropertyCompany";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<PropertyCompanyInfo> list = new List<PropertyCompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PropertyCompanyInfo model = new PropertyCompanyInfo();
                        model.Id = reader.GetGuid(1);
                        model.CompanyName = reader.GetString(2);
                        model.ShortName = reader.GetString(3);
                        model.Province = reader.GetString(4);
                        model.ProvinceCityId = reader.GetGuid(5);
                        model.City = reader.GetString(6);
                        model.District = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<PropertyCompanyInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate
                              from PropertyCompany";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;

            List<PropertyCompanyInfo> list = new List<PropertyCompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PropertyCompanyInfo model = new PropertyCompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyName = reader.GetString(1);
                        model.ShortName = reader.GetString(2);
                        model.Province = reader.GetString(3);
                        model.ProvinceCityId = reader.GetGuid(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<PropertyCompanyInfo> GetList()
        {
            string cmdText = @"select Id,CompanyName,ShortName,Province,ProvinceCityId,City,District,LastUpdatedDate 
			                from PropertyCompany
							order by LastUpdatedDate ";

            List<PropertyCompanyInfo> list = new List<PropertyCompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PropertyCompanyInfo model = new PropertyCompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyName = reader.GetString(1);
                        model.ShortName = reader.GetString(2);
                        model.Province = reader.GetString(3);
                        model.ProvinceCityId = reader.GetGuid(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
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
