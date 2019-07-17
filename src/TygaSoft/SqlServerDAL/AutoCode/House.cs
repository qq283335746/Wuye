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
    public partial class House : IHouse
    {
        #region 成员方法

        public int Insert(HouseInfo model)
        {
            if (IsExist(model.HouseCode, model.PropertyCompanyId, model.ResidenceCommunityId, model.ResidentialBuildingId, model.ResidentialUnitId, null)) return 110;

            string cmdText = @"insert into House (HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate)
			                 values
							 (@HouseCode,@PropertyCompanyId,@ResidenceCommunityId,@ResidentialBuildingId,@ResidentialUnitId,@HouseAcreage,@Remark,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@HouseCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialUnitId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseAcreage",SqlDbType.Float),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.HouseCode;
            parms[1].Value = model.PropertyCompanyId;
            parms[2].Value = model.ResidenceCommunityId;
            parms[3].Value = model.ResidentialBuildingId;
            parms[4].Value = model.ResidentialUnitId;
            parms[5].Value = model.HouseAcreage;
            parms[6].Value = model.Remark;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(HouseInfo model)
        {
            if (IsExist(model.HouseCode, model.PropertyCompanyId, model.ResidenceCommunityId, model.ResidentialBuildingId, model.ResidentialUnitId, model.Id)) return 110;

            string cmdText = @"update House set HouseCode = @HouseCode,PropertyCompanyId = @PropertyCompanyId,ResidenceCommunityId = @ResidenceCommunityId,ResidentialBuildingId = @ResidentialBuildingId,ResidentialUnitId = @ResidentialUnitId,HouseAcreage = @HouseAcreage,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialUnitId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseAcreage",SqlDbType.Float),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.HouseCode;
            parms[2].Value = model.PropertyCompanyId;
            parms[3].Value = model.ResidenceCommunityId;
            parms[4].Value = model.ResidentialBuildingId;
            parms[5].Value = model.ResidentialUnitId;
            parms[6].Value = model.HouseAcreage;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from House where Id = @Id";
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
                sb.Append(@"delete from House where Id = @Id" + n + " ;");
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

        public HouseInfo GetModel(object Id)
        {
            HouseInfo model = null;

            string cmdText = @"select top 1 Id,HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate 
			                   from House
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new HouseInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
                        model.ResidentialUnitId = reader.GetGuid(5);
                        model.HouseAcreage = reader.GetDouble(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public List<HouseInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from House ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate
					  from House ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseInfo> list = new List<HouseInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseInfo model = new HouseInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseCode = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.ResidentialBuildingId = reader.GetGuid(5);
                        model.ResidentialUnitId = reader.GetGuid(6);
                        model.HouseAcreage = reader.GetDouble(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate
							 from House";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseInfo> list = new List<HouseInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseInfo model = new HouseInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseCode = reader.GetString(2);
                        model.PropertyCompanyId = reader.GetGuid(3);
                        model.ResidenceCommunityId = reader.GetGuid(4);
                        model.ResidentialBuildingId = reader.GetGuid(5);
                        model.ResidentialUnitId = reader.GetGuid(6);
                        model.HouseAcreage = reader.GetDouble(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate
                              from House";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<HouseInfo> list = new List<HouseInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseInfo model = new HouseInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
                        model.ResidentialUnitId = reader.GetGuid(5);
                        model.HouseAcreage = reader.GetDouble(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseInfo> GetList()
        {
            string cmdText = @"select Id,HouseCode,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseAcreage,Remark,LastUpdatedDate 
			                from House
							order by LastUpdatedDate desc ";

            List<HouseInfo> list = new List<HouseInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseInfo model = new HouseInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseCode = reader.GetString(1);
                        model.PropertyCompanyId = reader.GetGuid(2);
                        model.ResidenceCommunityId = reader.GetGuid(3);
                        model.ResidentialBuildingId = reader.GetGuid(4);
                        model.ResidentialUnitId = reader.GetGuid(5);
                        model.HouseAcreage = reader.GetDouble(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
