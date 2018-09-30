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
        #region IResidentialBuilding Member

        public string[] GetBuildingByCommunityId(object communityId)
        {
            string cmdText = @"select Id
                              from ResidentialBuilding where ResidenceCommunityId = @ResidenceCommunityId ";

            SqlParameter parm = new SqlParameter("@ResidenceCommunityId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(communityId.ToString());

            StringBuilder sb = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    sb = new StringBuilder();
                    while (reader.Read())
                    {
                        sb.AppendFormat("{0},",reader.GetGuid(0));
                    }
                }
            }

            if (sb != null)
            {
                return sb.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[] { };
        }

        public ResidentialBuildingInfo GetModelByJoin(object Id)
        {
            ResidentialBuildingInfo model = null;

            string cmdText = @"select top 1 rb.Id,rb.BuildingCode,rb.CoveredArea,rb.PropertyCompanyId,rb.ResidenceCommunityId,rb.Remark,rb.LastUpdatedDate,
                               pc.CompanyName,rc.CommunityName 
			                   from ResidentialBuilding rb 
                               left join PropertyCompany pc on rb.PropertyCompanyId = pc.Id
                               left join ResidenceCommunity rc on rb.ResidenceCommunityId = rc.Id
							   where rb.Id = @Id ";
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
                        model.CompanyName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.CommunityName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    }
                }
            }

            return model;
        }

        public List<ResidentialBuildingInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidentialBuilding ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by rb.LastUpdatedDate desc) as RowNumber,
			          rb.Id,rb.BuildingCode,rb.CoveredArea,rb.PropertyCompanyId,rb.ResidenceCommunityId,rb.Remark,rb.LastUpdatedDate,
                      pc.CompanyName,rc.CommunityName 
					  from ResidentialBuilding rb
                      left join PropertyCompany pc on rb.PropertyCompanyId = pc.Id 
                      left join ResidenceCommunity rc on rb.ResidenceCommunityId = rc.Id
                      ";
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
                        model.CompanyName = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.CommunityName = reader.IsDBNull(9) ? "" : reader.GetString(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        private bool IsExist(string name, Guid companyId, Guid communityId, object Id)
        {
            Guid gId = Guid.Empty;
            if (Id != null)
            {
                Guid.TryParse(Id.ToString(), out gId);
            }

            ParamsHelper parms = new ParamsHelper();

            string cmdText = "select 1 from [ResidentialBuilding] where lower(BuildingCode) = @BuildingCode and PropertyCompanyId = @PropertyCompanyId and ResidenceCommunityId = @ResidenceCommunityId ";
            if (gId != Guid.Empty)
            {
                cmdText = "select 1 from [ResidentialBuilding] where lower(BuildingCode) = @BuildingCode and PropertyCompanyId = @PropertyCompanyId and ResidenceCommunityId = @ResidenceCommunityId and Id <> @Id ";
                SqlParameter parm1 = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(Id.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@BuildingCode", SqlDbType.VarChar, 50);
            parm.Value = name.ToLower();
            parms.Add(parm);
            parm = new SqlParameter("@PropertyCompanyId", SqlDbType.UniqueIdentifier);
            parm.Value = companyId;
            parms.Add(parm);
            parm = new SqlParameter("@ResidenceCommunityId", SqlDbType.UniqueIdentifier);
            parm.Value = communityId;
            parms.Add(parm);

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
