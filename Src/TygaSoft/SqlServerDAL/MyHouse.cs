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
        public string[] GetHouseByUnitId(object unitId)
        {
            string cmdText = @"select Id
                              from House where ResidentialUnitId = @ResidentialUnitId ";

            SqlParameter parm = new SqlParameter("@ResidentialUnitId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(unitId.ToString());

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

        public HouseInfo GetModelByJoin(object Id)
        {
            HouseInfo model = null;

            string cmdText = @"select top 1 h.Id,h.HouseCode,h.PropertyCompanyId,h.ResidenceCommunityId,h.ResidentialBuildingId,h.ResidentialUnitId,h.HouseAcreage,h.Remark,h.LastUpdatedDate,
                               pc.CompanyName,rc.CommunityName,rb.BuildingCode,ru.UnitCode
			                   from House h 
                               left join PropertyCompany pc on h.PropertyCompanyId = pc.Id
                               left join ResidenceCommunity rc on h.ResidenceCommunityId = rc.Id
                               left join ResidentialBuilding rb on h.ResidentialBuildingId = rb.Id
                               left join ResidentialUnit ru on h.ResidentialUnitId = ru.Id
							   where h.Id = @Id ";
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
                        model.CompanyName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.CommunityName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.BuildingCode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.UnitCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                    }
                }
            }

            return model;
        }

        public List<HouseInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from House ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by h.LastUpdatedDate desc) as RowNumber,
			          h.Id,HouseCode,h.PropertyCompanyId,h.ResidenceCommunityId,h.ResidentialBuildingId,h.ResidentialUnitId,h.HouseAcreage,h.Remark,h.LastUpdatedDate,
                      pc.CompanyName,rc.CommunityName,rb.BuildingCode,ru.UnitCode
					  from House h
                      left join PropertyCompany pc on h.PropertyCompanyId = pc.Id 
                      left join ResidenceCommunity rc on h.ResidenceCommunityId = rc.Id
                      left join ResidentialBuilding rb on h.ResidentialBuildingId = rb.Id
                      left join ResidentialUnit ru on h.ResidentialUnitId = ru.Id
                      ";
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
                        model.CompanyName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.CommunityName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.BuildingCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.UnitCode = reader.IsDBNull(13) ? "" : reader.GetString(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        private bool IsExist(string name, Guid companyId, Guid communityId, Guid buildingId, Guid unitId, object Id)
        {
            Guid gId = Guid.Empty;
            if (Id != null)
            {
                Guid.TryParse(Id.ToString(), out gId);
            }

            ParamsHelper parms = new ParamsHelper();

            string cmdText = @"select 1 from [House] where lower(HouseCode) = @HouseCode and PropertyCompanyId = @PropertyCompanyId 
                             and ResidenceCommunityId = @ResidenceCommunityId and ResidentialBuildingId = @ResidentialBuildingId 
                             and ResidentialUnitId = @ResidentialUnitId ";
            
            if (gId != Guid.Empty)
            {
                cmdText = @"select 1 from [House] where lower(HouseCode) = @HouseCode and PropertyCompanyId = @PropertyCompanyId 
                          and ResidenceCommunityId = @ResidenceCommunityId and ResidentialBuildingId = @ResidentialBuildingId 
                          and ResidentialUnitId = @ResidentialUnitId
                          and Id <> @Id ";
                SqlParameter parm1 = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(Id.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@HouseCode", SqlDbType.VarChar, 50);
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

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) return true;

            return false;
        }
    }
}
