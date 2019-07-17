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
    public partial class ResidenceCommunity : IResidenceCommunity
    {
        #region 成员方法

        public int Insert(ResidenceCommunityInfo model)
        {
            if (IsExist(model.CommunityName, model.PropertyCompanyId, null)) return 110;

            string cmdText = @"insert into ResidenceCommunity (CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate)
			                 values
							 (@CommunityName,@PropertyCompanyId,@ProvinceCityId,@Province,@City,@District,@Address,@AboutDescri,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@CommunityName",SqlDbType.NVarChar,30),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProvinceCityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Province",SqlDbType.NVarChar,10),
                                        new SqlParameter("@City",SqlDbType.NVarChar,10),
                                        new SqlParameter("@District",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Address",SqlDbType.NVarChar,30),
                                        new SqlParameter("@AboutDescri",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.CommunityName;
            parms[1].Value = model.PropertyCompanyId;
            parms[2].Value = model.ProvinceCityId;
            parms[3].Value = model.Province;
            parms[4].Value = model.City;
            parms[5].Value = model.District;
            parms[6].Value = model.Address;
            parms[7].Value = model.AboutDescri;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ResidenceCommunityInfo model)
        {
            if (IsExist(model.CommunityName, model.PropertyCompanyId, model.Id)) return 110;

            string cmdText = @"update ResidenceCommunity set CommunityName = @CommunityName,PropertyCompanyId = @PropertyCompanyId,ProvinceCityId = @ProvinceCityId,Province = @Province,City = @City,District = @District,Address = @Address,AboutDescri = @AboutDescri,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CommunityName",SqlDbType.NVarChar,30),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProvinceCityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Province",SqlDbType.NVarChar,10),
                                        new SqlParameter("@City",SqlDbType.NVarChar,10),
                                        new SqlParameter("@District",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Address",SqlDbType.NVarChar,30),
                                        new SqlParameter("@AboutDescri",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CommunityName;
            parms[2].Value = model.PropertyCompanyId;
            parms[3].Value = model.ProvinceCityId;
            parms[4].Value = model.Province;
            parms[5].Value = model.City;
            parms[6].Value = model.District;
            parms[7].Value = model.Address;
            parms[8].Value = model.AboutDescri;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ResidenceCommunity where Id = @Id";
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
                sb.Append(@"delete from ResidenceCommunity where Id = @Id" + n + " ;");
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

        public ResidenceCommunityInfo GetModel(object Id)
        {
            ResidenceCommunityInfo model = null;

            string cmdText = @"select top 1 Id,CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate 
			                   from ResidenceCommunity
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ResidenceCommunityInfo();
                        model.Id = reader.GetGuid(0);
                        model.CommunityName = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ProvinceCityId = reader.GetGuid(3);
                        model.Province = reader.GetString(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
                        model.Address = reader.GetString(7);
                        model.AboutDescri = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                    }
                }
            }

            return model;
        }

        public List<ResidenceCommunityInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidenceCommunity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate
					  from ResidenceCommunity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidenceCommunityInfo> list = new List<ResidenceCommunityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidenceCommunityInfo model = new ResidenceCommunityInfo();
                        model.Id = reader.GetGuid(1);
                        model.CommunityName = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ProvinceCityId = reader.GetGuid(4);
                        model.Province = reader.GetString(5);
                        model.City = reader.GetString(6);
                        model.District = reader.GetString(7);
                        model.Address = reader.GetString(8);
                        model.AboutDescri = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidenceCommunityInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate
							 from ResidenceCommunity";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidenceCommunityInfo> list = new List<ResidenceCommunityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidenceCommunityInfo model = new ResidenceCommunityInfo();
                        model.Id = reader.GetGuid(1);
                        model.CommunityName = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ProvinceCityId = reader.GetGuid(4);
                        model.Province = reader.GetString(5);
                        model.City = reader.GetString(6);
                        model.District = reader.GetString(7);
                        model.Address = reader.GetString(8);
                        model.AboutDescri = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidenceCommunityInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate
                              from ResidenceCommunity";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<ResidenceCommunityInfo> list = new List<ResidenceCommunityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidenceCommunityInfo model = new ResidenceCommunityInfo();
                        model.Id = reader.GetGuid(0);
                        model.CommunityName = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ProvinceCityId = reader.GetGuid(3);
                        model.Province = reader.GetString(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
                        model.Address = reader.GetString(7);
                        model.AboutDescri = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidenceCommunityInfo> GetList()
        {
            string cmdText = @"select Id,CommunityName,PropertyCompanyId,ProvinceCityId,Province,City,District,Address,AboutDescri,LastUpdatedDate 
			                from ResidenceCommunity
							order by LastUpdatedDate desc ";

            List<ResidenceCommunityInfo> list = new List<ResidenceCommunityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidenceCommunityInfo model = new ResidenceCommunityInfo();
                        model.Id = reader.GetGuid(0);
                        model.CommunityName = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ProvinceCityId = reader.GetGuid(3);
                        model.Province = reader.GetString(4);
                        model.City = reader.GetString(5);
                        model.District = reader.GetString(6);
                        model.Address = reader.GetString(7);
                        model.AboutDescri = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
