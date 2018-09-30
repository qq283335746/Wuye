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
        public string[] GetCommunityByCompanyId(object companyId)
        {
            string cmdText = @"select Id
                              from ResidenceCommunity where PropertyCompanyId = @PropertyCompanyId ";

            SqlParameter parm = new SqlParameter("@PropertyCompanyId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(companyId.ToString());

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
                return sb.ToString().Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[]{};
        }

        public ResidenceCommunityInfo GetModelByJoin(object Id)
        {
            ResidenceCommunityInfo model = null;

            string cmdText = @"select top 1 rc.Id,rc.CommunityName,rc.PropertyCompanyId,rc.ProvinceCityId,rc.Province,rc.City,rc.District,rc.Address,rc.AboutDescri,rc.LastUpdatedDate, 
                               pc.CompanyName
			                   from ResidenceCommunity rc left join PropertyCompany pc on rc.PropertyCompanyId = pc.Id
							   where rc.Id = @Id ";
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
                        model.CompanyName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                    }
                }
            }

            return model;
        }

        public List<ResidenceCommunityInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from ResidenceCommunity ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by rc.LastUpdatedDate desc) as RowNumber,
			          rc.Id,rc.CommunityName,rc.PropertyCompanyId,rc.ProvinceCityId,rc.Province,rc.City,rc.District,rc.Address,rc.AboutDescri,rc.LastUpdatedDate,
                      pc.CompanyName
					  from ResidenceCommunity rc left join PropertyCompany pc on rc.PropertyCompanyId = pc.Id ";
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
                        model.CompanyName = reader.IsDBNull(11) ? "" : reader.GetString(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 是否存在对应数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="companyId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool IsExist(string name, Guid companyId, object Id)
        {
            Guid gId = Guid.Empty;
            if (Id != null)
            {
                Guid.TryParse(Id.ToString(), out gId);
            }

            ParamsHelper parms = new ParamsHelper();

            string cmdText = "select 1 from [ResidenceCommunity] where lower(CommunityName) = @CommunityName and PropertyCompanyId = @PropertyCompanyId";
            if (gId != Guid.Empty)
            {
                cmdText = "select 1 from [ResidenceCommunity] where lower(CommunityName) = @CommunityName and PropertyCompanyId = @PropertyCompanyId and Id <> @Id ";
                SqlParameter parm1 = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm1.Value = Guid.Parse(Id.ToString());
                parms.Add(parm1);
            }
            SqlParameter parm = new SqlParameter("@CommunityName", SqlDbType.NVarChar, 50);
            parm.Value = name.ToLower();
            parms.Add(parm);
            parm = new SqlParameter("@PropertyCompanyId", SqlDbType.UniqueIdentifier);
            parm.Value = companyId;
            parms.Add(parm);

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms.ToArray());
            if (obj != null) return true;

            return false;
        }
    }
}
