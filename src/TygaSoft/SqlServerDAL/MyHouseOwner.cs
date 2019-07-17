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
        public string[] GetHouseOwnerByHouseId(object houseId)
        {
            string cmdText = @"select Id
                              from HouseOwner where HouseId = @HouseId ";

            SqlParameter parm = new SqlParameter("@HouseId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(houseId.ToString());

            StringBuilder sb = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    sb = new StringBuilder();
                    while (reader.Read())
                    {
                        sb.AppendFormat("{0},", reader.GetGuid(0));
                    }
                }
            }

            if (sb != null)
            {
                return sb.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[] { };
        }

        public HouseOwnerInfo GetModelByJoin(object Id)
        {
            //left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = ho.UserId

            HouseOwnerInfo model = null;

            string cmdText = @"select top 1 ho.Id,ho.HouseOwnerName,ho.MobilePhone,ho.TelPhone,ho.PropertyCompanyId,ho.ResidenceCommunityId,ho.ResidentialBuildingId,ho.ResidentialUnitId,ho.HouseId,ho.LastUpdatedDate,
                               pc.CompanyName,rc.CommunityName,rb.BuildingCode,ru.UnitCode,h.HouseCode
			                   from HouseOwner ho 
                               left join PropertyCompany pc on ho.PropertyCompanyId = pc.Id
                               left join ResidenceCommunity rc on ho.ResidenceCommunityId = rc.Id
                               left join ResidentialBuilding rb on ho.ResidentialBuildingId = rb.Id
                               left join ResidentialUnit ru on ho.ResidentialUnitId = ru.Id
                               left join House h on ho.HouseId = h.Id
							   where ho.Id = @Id ";
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
                        model.CompanyName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.CommunityName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.BuildingCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.UnitCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.HouseCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        //model.UserName = reader.IsDBNull(15) ? "" : reader.GetString(15);
                    }
                }
            }

            return model;
        }

        public List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*)
                             from HouseOwner ho
                             left join PropertyCompany pc on ho.PropertyCompanyId = pc.Id 
                             left join ResidenceCommunity rc on ho.ResidenceCommunityId = rc.Id
                             left join ResidentialBuilding rb on ho.ResidentialBuildingId = rb.Id
                             left join ResidentialUnit ru on ho.ResidentialUnitId = ru.Id
                             left join House h on ho.HouseId = h.Id
                             left join UserHouseOwner uho on uho.HouseOwnerId = ho.Id
                             left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = uho.UserId
                             ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by ho.LastUpdatedDate desc) as RowNumber,
			          ho.Id,ho.HouseOwnerName,ho.MobilePhone,ho.TelPhone,ho.PropertyCompanyId,ho.ResidenceCommunityId,ho.ResidentialBuildingId,ho.ResidentialUnitId,ho.HouseId,ho.LastUpdatedDate,
                      pc.CompanyName,rc.CommunityName,rb.BuildingCode,ru.UnitCode,h.HouseCode,u.UserName,uho.Password
					  from HouseOwner ho
                      left join PropertyCompany pc on ho.PropertyCompanyId = pc.Id 
                      left join ResidenceCommunity rc on ho.ResidenceCommunityId = rc.Id
                      left join ResidentialBuilding rb on ho.ResidentialBuildingId = rb.Id
                      left join ResidentialUnit ru on ho.ResidentialUnitId = ru.Id
                      left join House h on ho.HouseId = h.Id
                      left join UserHouseOwner uho on uho.HouseOwnerId = ho.Id
                      left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = uho.UserId
                      ";

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
                        model.CompanyName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CommunityName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.BuildingCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.UnitCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.HouseCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.UserName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.Password = reader.IsDBNull(17) ? "" : reader.GetString(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, string orderBy, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*)
                             from HouseOwner ho
                             left join PropertyCompany pc on ho.PropertyCompanyId = pc.Id 
                             left join ResidenceCommunity rc on ho.ResidenceCommunityId = rc.Id
                             left join ResidentialBuilding rb on ho.ResidentialBuildingId = rb.Id
                             left join ResidentialUnit ru on ho.ResidentialUnitId = ru.Id
                             left join House h on ho.HouseId = h.Id
                             left join UserHouseOwner uho on uho.HouseOwnerId = ho.Id
                             left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = uho.UserId
                             ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            if (string.IsNullOrWhiteSpace(orderBy)) orderBy = "ho.LastUpdatedDate";
            cmdText = @"select * from(select row_number() over(order by {0} desc) as RowNumber,
			          ho.Id,ho.HouseOwnerName,ho.MobilePhone,ho.TelPhone,ho.PropertyCompanyId,ho.ResidenceCommunityId,ho.ResidentialBuildingId,ho.ResidentialUnitId,ho.HouseId,ho.LastUpdatedDate,
                      pc.CompanyName,rc.CommunityName,rb.BuildingCode,ru.UnitCode,h.HouseCode,u.UserName,uho.Password
					  from HouseOwner ho
                      left join PropertyCompany pc on ho.PropertyCompanyId = pc.Id 
                      left join ResidenceCommunity rc on ho.ResidenceCommunityId = rc.Id
                      left join ResidentialBuilding rb on ho.ResidentialBuildingId = rb.Id
                      left join ResidentialUnit ru on ho.ResidentialUnitId = ru.Id
                      left join House h on ho.HouseId = h.Id
                      left join UserHouseOwner uho on uho.HouseOwnerId = ho.Id
                      left join CollectLifeAspnetDb.dbo.aspnet_Users u on u.UserId = uho.UserId
                      ";
            cmdText = string.Format(cmdText, orderBy);
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
                        model.CompanyName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CommunityName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.BuildingCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.UnitCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.HouseCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.UserName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.Password = reader.IsDBNull(17) ? "" : reader.GetString(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        private bool IsExist(string name, Guid companyId, Guid communityId, Guid buildingId, Guid unitId,Guid houseId, object Id)
        {
            Guid gId = Guid.Empty;
            if (Id != null)
            {
                Guid.TryParse(Id.ToString(), out gId);
            }

            ParamsHelper parms = new ParamsHelper();

            string cmdText = @"select 1 from [HouseOwner] where lower(HouseOwnerName) = @HouseOwnerName and PropertyCompanyId = @PropertyCompanyId 
                             and ResidenceCommunityId = @ResidenceCommunityId and ResidentialBuildingId = @ResidentialBuildingId 
                             and ResidentialUnitId = @ResidentialUnitId and HouseId = @HouseId ";

            if (gId != Guid.Empty)
            {
                cmdText = @"select 1 from [HouseOwner] where lower(HouseOwnerName) = @HouseOwnerName and PropertyCompanyId = @PropertyCompanyId 
                          and ResidenceCommunityId = @ResidenceCommunityId and ResidentialBuildingId = @ResidentialBuildingId 
                          and ResidentialUnitId = @ResidentialUnitId and HouseId = @HouseId
                          and Id <> @Id ";
                SqlParameter parm1 = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(Id.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@HouseOwnerName", SqlDbType.VarChar, 50);
            parm.Value = name.ToLower();
            parms.Add(parm);
            parm = new SqlParameter("@PropertyCompanyId", SqlDbType.UniqueIdentifier);
            parm.Value = companyId;
            parms.Add(parm);
            parm = new SqlParameter("@ResidenceCommunityId", SqlDbType.UniqueIdentifier);
            parm.Value = communityId;
            parms.Add(parm);
            parm = new SqlParameter("@ResidentialBuildingId", SqlDbType.UniqueIdentifier);
            parm.Value = buildingId;
            parms.Add(parm);
            parm = new SqlParameter("@ResidentialUnitId", SqlDbType.UniqueIdentifier);
            parm.Value = unitId;
            parms.Add(parm);
            parm = new SqlParameter("@HouseId", SqlDbType.UniqueIdentifier);
            parm.Value = houseId;
            parms.Add(parm);

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) return true;

            return false;
        }
    }
}
