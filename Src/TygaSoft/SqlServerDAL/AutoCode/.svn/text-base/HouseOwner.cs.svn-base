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
    public partial class HouseOwner : IHouseOwner
    {
        #region 成员方法

        public int Insert(HouseOwnerInfo model)
        {
            if (IsExist(model.HouseOwnerName, model.PropertyCompanyId, model.ResidenceCommunityId,model.ResidentialBuildingId, model.ResidentialUnitId, model.HouseId, null)) return 110;

            string cmdText = @"insert into HouseOwner (HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate)
			                 values
							 (@HouseOwnerName,@MobilePhone,@TelPhone,@PropertyCompanyId,@ResidenceCommunityId,@ResidentialBuildingId,@ResidentialUnitId,@HouseId,@LastUpdatedDate)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@HouseOwnerName",SqlDbType.NVarChar,30),
                                        new SqlParameter("@MobilePhone",SqlDbType.Char,15),
                                        new SqlParameter("@TelPhone",SqlDbType.Char,15),
                                        new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ResidentialUnitId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.HouseOwnerName;
            parms[1].Value = model.MobilePhone;
            parms[2].Value = model.TelPhone;
            parms[3].Value = model.PropertyCompanyId;
            parms[4].Value = model.ResidenceCommunityId;
            parms[5].Value = model.ResidentialBuildingId;
            parms[6].Value = model.ResidentialUnitId;
            parms[7].Value = model.HouseId;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(HouseOwnerInfo model)
        {
            if (IsExist(model.HouseOwnerName, model.PropertyCompanyId, model.ResidenceCommunityId, model.ResidentialBuildingId, model.ResidentialUnitId, model.HouseId, model.Id)) return 110;

            string cmdText = @"update HouseOwner set HouseOwnerName = @HouseOwnerName,MobilePhone = @MobilePhone,TelPhone = @TelPhone,PropertyCompanyId = @PropertyCompanyId,ResidenceCommunityId = @ResidenceCommunityId,ResidentialBuildingId = @ResidentialBuildingId,ResidentialUnitId = @ResidentialUnitId,HouseId = @HouseId,LastUpdatedDate = @LastUpdatedDate 
			                 where Id = @Id";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@HouseOwnerName",SqlDbType.NVarChar,30),
                                    new SqlParameter("@MobilePhone",SqlDbType.Char,15),
                                    new SqlParameter("@TelPhone",SqlDbType.Char,15),
                                    new SqlParameter("@PropertyCompanyId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ResidenceCommunityId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ResidentialBuildingId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ResidentialUnitId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@HouseId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.HouseOwnerName;
            parms[2].Value = model.MobilePhone;
            parms[3].Value = model.TelPhone;
            parms[4].Value = model.PropertyCompanyId;
            parms[5].Value = model.ResidenceCommunityId;
            parms[6].Value = model.ResidentialBuildingId;
            parms[7].Value = model.ResidentialUnitId;
            parms[8].Value = model.HouseId;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object Id)
        {
            string cmdText = "delete from HouseOwner where Id = @Id";
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
                sb.Append(@"delete from HouseOwner where Id = @Id" + n + " ;");
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

        public HouseOwnerInfo GetModel(object Id)
        {
            HouseOwnerInfo model = null;

            string cmdText = @"select top 1 Id,HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate 
			                   from HouseOwner
							   where Id = @Id ";
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new HouseOwnerInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerName = reader.GetString(1);
                        model.MobilePhone = reader.GetString(2);
                        model.TelPhone = reader.GetString(3);
                        model.PropertyCompanyId = reader.GetGuid(4);
                        model.ResidenceCommunityId = reader.GetGuid(5);
                        model.ResidentialBuildingId = reader.GetGuid(6);
                        model.ResidentialUnitId = reader.GetGuid(7);
                        model.HouseId = reader.GetGuid(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                    }
                }
            }

            return model;
        }

        public List<HouseOwnerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from HouseOwner ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate
					  from HouseOwner ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseOwnerInfo> list = new List<HouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerInfo model = new HouseOwnerInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseOwnerName = reader.GetString(2);
                        model.MobilePhone = reader.GetString(3);
                        model.TelPhone = reader.GetString(4);
                        model.PropertyCompanyId = reader.GetGuid(5);
                        model.ResidenceCommunityId = reader.GetGuid(6);
                        model.ResidentialBuildingId = reader.GetGuid(7);
                        model.ResidentialUnitId = reader.GetGuid(8);
                        model.HouseId = reader.GetGuid(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 Id,HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate
							 from HouseOwner";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseOwnerInfo> list = new List<HouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerInfo model = new HouseOwnerInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseOwnerName = reader.GetString(2);
                        model.MobilePhone = reader.GetString(3);
                        model.TelPhone = reader.GetString(4);
                        model.PropertyCompanyId = reader.GetGuid(5);
                        model.ResidenceCommunityId = reader.GetGuid(6);
                        model.ResidentialBuildingId = reader.GetGuid(7);
                        model.ResidentialUnitId = reader.GetGuid(8);
                        model.HouseId = reader.GetGuid(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select Id,HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate
                              from HouseOwner";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<HouseOwnerInfo> list = new List<HouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerInfo model = new HouseOwnerInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerName = reader.GetString(1);
                        model.MobilePhone = reader.GetString(2);
                        model.TelPhone = reader.GetString(3);
                        model.PropertyCompanyId = reader.GetGuid(4);
                        model.ResidenceCommunityId = reader.GetGuid(5);
                        model.ResidentialBuildingId = reader.GetGuid(6);
                        model.ResidentialUnitId = reader.GetGuid(7);
                        model.HouseId = reader.GetGuid(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerInfo> GetList()
        {
            string cmdText = @"select Id,HouseOwnerName,MobilePhone,TelPhone,PropertyCompanyId,ResidenceCommunityId,ResidentialBuildingId,ResidentialUnitId,HouseId,LastUpdatedDate 
			                from HouseOwner
							order by LastUpdatedDate desc ";

            List<HouseOwnerInfo> list = new List<HouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerInfo model = new HouseOwnerInfo();
                        model.Id = reader.GetGuid(0);
                        model.HouseOwnerName = reader.GetString(1);
                        model.MobilePhone = reader.GetString(2);
                        model.TelPhone = reader.GetString(3);
                        model.PropertyCompanyId = reader.GetGuid(4);
                        model.ResidenceCommunityId = reader.GetGuid(5);
                        model.ResidentialBuildingId = reader.GetGuid(6);
                        model.ResidentialUnitId = reader.GetGuid(7);
                        model.HouseId = reader.GetGuid(8);
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
