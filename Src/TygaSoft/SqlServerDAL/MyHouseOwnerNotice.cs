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
    public partial class HouseOwnerNotice : IHouseOwnerNotice
    {
        public void BulkCopy(DataTable dt)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.SqlProviderConnString))
            {
                bulkCopy.DestinationTableName = "dbo.HouseOwnerNotice";
                bulkCopy.WriteToServer(dt);
            }
        }

        public List<HouseOwnerNoticeInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from HouseOwnerNotice hon
                               left join HouseOwner ho on ho.Id = hon.HouseOwnerId
                               left join Notice n on n.Id = hon.NoticeId
                               
                              ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by hon.LastUpdatedDate desc) as RowNumber,
			          hon.Id,hon.HouseOwnerId,hon.NoticeId,hon.Status,hon.IsRead,hon.LastUpdatedDate,
                      ho.HouseOwnerName,ho.MobilePhone,ho.TelPhone,n.Title 
					  from HouseOwnerNotice hon
                      left join HouseOwner ho on ho.Id = hon.HouseOwnerId
                      left join Notice n on n.Id = hon.NoticeId
                      ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<HouseOwnerNoticeInfo> list = new List<HouseOwnerNoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseOwnerNoticeInfo model = new HouseOwnerNoticeInfo();
                        model.Id = reader.GetGuid(1);
                        model.HouseOwnerId = reader.GetGuid(2);
                        model.NoticeId = reader.GetGuid(3);
                        model.Status = reader.GetByte(4);
                        model.IsRead = reader.GetBoolean(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                        model.HouseOwnerName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.MobilePhone = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.TelPhone = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.NoticeTitle = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
