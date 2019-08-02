using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysHelper;

namespace TygaSoft.SqlServerDAL
{
    public partial class UserHouseOwner : IUserHouseOwner
    {
        public int Delete(object userId, object houseOwnerId)
        {
            string cmdText = "delete from UserHouseOwner where UserId = @UserId and HouseOwnerId = @HouseOwnerId ";

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = Guid.Parse(userId.ToString());
            parms[1].Value = Guid.Parse(houseOwnerId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = string.Format(@"select count(*) from UserHouseOwner uho
                               join HouseOwner ho on ho.Id = uho.HouseOwnerId
                               join {0}aspnet_Users u on u.UserId = uho.UserId
                              ", ConfigHelper.AspnetDbDbo);
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = string.Format(@"select * from(select row_number() over(order by ho.LastUpdatedDate desc) as RowNumber,
			          ho.Id,ho.HouseOwnerName,ho.MobilePhone,ho.TelPhone
					  from UserHouseOwner uho
                      join HouseOwner ho on ho.Id = uho.HouseOwnerId
                      join {0}aspnet_Users u on u.UserId = uho.UserId
                      ", ConfigHelper.AspnetDbDbo);
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        private bool IsExist(object userId, object houseOwnerId)
        {
            string cmdText = @"select 1 from [UserHouseOwner] where UserId = @UserId and HouseOwnerId = @HouseOwnerId ";

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = Guid.Parse(userId.ToString());
            parms[1].Value = Guid.Parse(houseOwnerId.ToString());

            object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }
    }
}
