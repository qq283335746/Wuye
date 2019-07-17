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
    public partial class ResidentialBuilding : IResidentialBuilding
    {
        #region 成员方法

        public int Insert(ResidentialBuildingInfo model)
        {
            if (IsExist(model.BuildingCode, model.PropertyCompanyId, model.ResidenceCommunityId, null)) return 110;

            string cmdText = @"insert into ResidentialBuilding (BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate)
			                 values
							 (@BuildingCode,@CoveredArea,@PropertyCompanyId,@ResidenceCommunityId,@Remark,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@BuildingCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@CoveredArea",SqlDbType.Float),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.BuildingCode;
            parms[1].Value = model.CoveredArea;
            parms[2].Value = model.PropertyCompanyId;
            parms[3].Value = model.ResidenceCommunityId;
            parms[4].Value = model.Remark;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ResidentialBuildingInfo model)
        {
            if (IsExist(model.BuildingCode, model.PropertyCompanyId, model.ResidenceCommunityId, model.Id)) return 110;

            string cmdText = @"update ResidentialBuilding set BuildingCode = @BuildingCode,CoveredArea = @CoveredArea,PropertyCompanyId = @PropertyCompanyId,ResidenceCommunityId = @ResidenceCommunityId,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@BuildingCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@CoveredArea",SqlDbType.Float),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.BuildingCode;
            parms[2].Value = model.CoveredArea;
            parms[3].Value = model.PropertyCompanyId;
            parms[4].Value = model.ResidenceCommunityId;
            parms[5].Value = model.Remark;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ResidentialBuilding where Id = @Id";
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
                sb.Append(@"delete from ResidentialBuilding where Id = @Id" + n + " ;");
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

        public ResidentialBuildingInfo GetModel(object Id)
        {
            ResidentialBuildingInfo model = null;

            string cmdText = @"select top 1 Id,BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate 
			                   from ResidentialBuilding
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ResidentialBuildingInfo();
                        model.Id = reader.GetGuid(0);
                        model.BuildingCode = reader.GetString(1);
                        model.CoveredArea = reader.GetDouble(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.Remark = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public List<ResidentialBuildingInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidentialBuilding ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate
					  from ResidentialBuilding ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidentialBuildingInfo> list = new List<ResidentialBuildingInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialBuildingInfo model = new ResidentialBuildingInfo();
                        model.Id = reader.GetGuid(1);
                        model.BuildingCode = reader.GetString(2);
                        model.CoveredArea = reader.GetDouble(3);
                        model.PropertyCompanyId = reader.GetGuid(4);
                        model.ResidenceCommunityId = reader.GetGuid(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialBuildingInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate
							 from ResidentialBuilding";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidentialBuildingInfo> list = new List<ResidentialBuildingInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialBuildingInfo model = new ResidentialBuildingInfo();
                        model.Id = reader.GetGuid(1);
                        model.BuildingCode = reader.GetString(2);
                        model.CoveredArea = reader.GetDouble(3);
                        model.PropertyCompanyId = reader.GetGuid(4);
                        model.ResidenceCommunityId = reader.GetGuid(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialBuildingInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate
                              from ResidentialBuilding";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<ResidentialBuildingInfo> list = new List<ResidentialBuildingInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialBuildingInfo model = new ResidentialBuildingInfo();
                        model.Id = reader.GetGuid(0);
                        model.BuildingCode = reader.GetString(1);
                        model.CoveredArea = reader.GetDouble(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.Remark = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialBuildingInfo> GetList()
        {
            string cmdText = @"select Id,BuildingCode,CoveredArea,PropertyCompanyId,ResidenceCommunityId,Remark,LastUpdatedDate 
			                from ResidentialBuilding
							order by LastUpdatedDate desc ";

            List<ResidentialBuildingInfo> list = new List<ResidentialBuildingInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialBuildingInfo model = new ResidentialBuildingInfo();
                        model.Id = reader.GetGuid(0);
                        model.BuildingCode = reader.GetString(1);
                        model.CoveredArea = reader.GetDouble(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.Remark = reader.GetString(5);
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
