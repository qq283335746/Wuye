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
        public string[] GetUnitByBuildingId(object buildingId)
        {
            string cmdText = @"select Id
                              from ResidentialUnit where ResidentialBuildingId = @ResidentialBuildingId ";

            SqlParameter parm = new SqlParameter("@ResidentialBuildingId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(buildingId.ToString());

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
        
        public ResidentialUnitInfo GetModelByJoin(object Id)
        {
            ResidentialUnitInfo model = null;

            string cmdText = @"select top 1 ru.Id,ru.UnitCode,ru.PropertyCompanyId,ru.ResidenceCommunityId,ru.ResidentialBuildingId,ru.Remark,ru.LastUpdatedDate,
                               pc.CompanyName,rc.CommunityName,rb.BuildingCode 
			                   from ResidentialUnit ru 
                               left join PropertyCompany pc on ru.PropertyCompanyId = pc.Id
                               left join ResidenceCommunity rc on ru.ResidenceCommunityId = rc.Id
                               left join ResidentialBuilding rb on ru.ResidentialBuildingId = rb.Id
							   where ru.Id = @Id ";
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
                        model.CompanyName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.CommunityName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.BuildingCode = reader.IsDBNull(9) ? "" : reader.GetString(9);
                    }
                }
            }

            return model;
        }

        public List<ResidentialUnitInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidentialUnit ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by ru.LastUpdatedDate desc) as RowNumber,
			          ru.Id,ru.UnitCode,ru.PropertyCompanyId,ru.ResidenceCommunityId,ru.ResidentialBuildingId,ru.Remark,ru.LastUpdatedDate,
                      pc.CompanyName,rc.CommunityName,rb.BuildingCode 
					  from ResidentialUnit ru
                      left join PropertyCompany pc on ru.PropertyCompanyId = pc.Id 
                      left join ResidenceCommunity rc on ru.ResidenceCommunityId = rc.Id
                      left join ResidentialBuilding rb on ru.ResidentialBuildingId = rb.Id
                      ";
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
                        model.CompanyName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.CommunityName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.BuildingCode = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        private bool IsExist(string name, Guid companyId, Guid communityId, Guid buildingId, object Id)
        {
            Guid gId = Guid.Empty;
            if (Id != null)
            {
                Guid.TryParse(Id.ToString(), out gId);
            }

            ParamsHelper parms = new ParamsHelper();

            string cmdText = @"select 1 from [ResidentialUnit] where lower(UnitCode) = @UnitCode 
                             and PropertyCompanyId = @PropertyCompanyId and ResidenceCommunityId = @ResidenceCommunityId 
                             and ResidentialBuildingId = @ResidentialBuildingId ";
            if (gId != Guid.Empty)
            {
                cmdText = @"select 1 from [ResidentialUnit] where lower(UnitCode) = @UnitCode and PropertyCompanyId = @PropertyCompanyId 
                          and ResidenceCommunityId = @ResidenceCommunityId and ResidentialBuildingId = @ResidentialBuildingId and Id <> @Id ";
                SqlParameter parm1 = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(Id.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@UnitCode", SqlDbType.VarChar, 50);
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

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) return true;

            return false;
        }
    }
}
