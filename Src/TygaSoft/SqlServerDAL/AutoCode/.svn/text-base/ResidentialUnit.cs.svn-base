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
    public partial class ResidentialUnit : IResidentialUnit
    {
        #region 成员方法

        public int Insert(ResidentialUnitInfo model)
        {
            if (IsExist(model.UnitCode, model.PropertyCompanyId, model.ResidenceCommunityId, model.ResidentialBuildingId, null)) return 110;

            string cmdText = @"insert into ResidentialUnit (UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate)
			                 values
							 (@UnitCode,@PropertyCompanyId,@ResidenceCommunityId,@ResidentialBuildingId,@Remark,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@UnitCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UnitCode;
            parms[1].Value = model.PropertyCompanyId;
            parms[2].Value = model.ResidenceCommunityId;
            parms[3].Value = model.ResidentialBuildingId;
            parms[4].Value = model.Remark;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(ResidentialUnitInfo model)
        {
            if (IsExist(model.UnitCode, model.PropertyCompanyId, model.ResidenceCommunityId, model.ResidentialBuildingId, model.Id)) return 110;

            string cmdText = @"update ResidentialUnit set UnitCode = @UnitCode,PropertyCompanyId = @PropertyCompanyId,ResidenceCommunityId = @ResidenceCommunityId,ResidentialBuildingId = @ResidentialBuildingId,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UnitCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UnitCode;
            parms[2].Value = model.PropertyCompanyId;
            parms[3].Value = model.ResidenceCommunityId;
            parms[4].Value = model.ResidentialBuildingId;
            parms[5].Value = model.Remark;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from ResidentialUnit where Id = @Id";
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
                sb.Append(@"delete from ResidentialUnit where Id = @Id" + n + " ;");
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

        public ResidentialUnitInfo GetModel(object Id)
        {
            ResidentialUnitInfo model = null;

            string cmdText = @"select top 1 Id,UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate 
			                   from ResidentialUnit
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ResidentialUnitInfo();
                        model.Id = reader.GetGuid(0);
                        model.UnitCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
                        model.Remark = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public List<ResidentialUnitInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidentialUnit ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate
					  from ResidentialUnit ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidentialUnitInfo> list = new List<ResidentialUnitInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialUnitInfo model = new ResidentialUnitInfo();
                        model.Id = reader.GetGuid(1);
                        model.UnitCode = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.ResidentialBuildingId = reader.GetGuid(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialUnitInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate
							 from ResidentialUnit";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<ResidentialUnitInfo> list = new List<ResidentialUnitInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialUnitInfo model = new ResidentialUnitInfo();
                        model.Id = reader.GetGuid(1);
                        model.UnitCode = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.ResidentialBuildingId = reader.GetGuid(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialUnitInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate
                              from ResidentialUnit";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<ResidentialUnitInfo> list = new List<ResidentialUnitInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialUnitInfo model = new ResidentialUnitInfo();
                        model.Id = reader.GetGuid(0);
                        model.UnitCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
                        model.Remark = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<ResidentialUnitInfo> GetList()
        {
            string cmdText = @"select Id,UnitCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,Remark,LastUpdatedDate 
			                from ResidentialUnit
							order by LastUpdatedDate desc ";

            List<ResidentialUnitInfo> list = new List<ResidentialUnitInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResidentialUnitInfo model = new ResidentialUnitInfo();
                        model.Id = reader.GetGuid(0);
                        model.UnitCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
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
