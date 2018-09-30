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
    public partial class Notice
    {
        #region INotice Member

        public NoticeInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            NoticeInfo model = null;

            string cmdText = @"select top 1 n.Id,n.ContentTypeId,n.Title,n.Descr,n.ContentText,n.LastUpdatedDate
			                   from Notice n
                              join HouseOwnerNotice hon on hon.NoticeId = n.Id
                              join HouseOwner ho on ho.Id = hon.HouseOwnerId
                              join UserHouseOwner uho on uho.HouseOwnerId = hon.HouseOwnerId
							  ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new NoticeInfo();
                        model.Id = reader.GetGuid(0);
                        model.ContentTypeId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.Descr = reader.GetString(3);
                        model.ContentText = reader.GetString(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);
                    }
                }
            }

            return model;
        }

        public IList<NoticeInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from Notice n
                             join HouseOwnerNotice hon on hon.NoticeId = n.Id
                             join HouseOwner ho on ho.Id = hon.HouseOwnerId
                             join UserHouseOwner uho on uho.HouseOwnerId = hon.HouseOwnerId
                             ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            if (totalRecords == 0) return null;

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by n.LastUpdatedDate desc) as RowNumber,
			          n.Id,n.ContentTypeId,n.Title,n.Descr,n.LastUpdatedDate
					  from Notice n
                      join HouseOwnerNotice hon on hon.NoticeId = n.Id
                      join HouseOwner ho on ho.Id = hon.HouseOwnerId
                      join UserHouseOwner uho on uho.HouseOwnerId = hon.HouseOwnerId
                      ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<NoticeInfo> list = new List<NoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        NoticeInfo model = new NoticeInfo();
                        model.Id = reader.GetGuid(1);
                        model.ContentTypeId = reader.GetGuid(2);
                        model.Title = reader.GetString(3);
                        model.Descr = reader.GetString(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<NoticeInfo> GetListExceptContent(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from Notice ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,ContentTypeId,Title,Descr,LastUpdatedDate
					  from Notice ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            IList<NoticeInfo> list = new List<NoticeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        NoticeInfo model = new NoticeInfo();
                        model.Id = reader.GetGuid(1);
                        model.ContentTypeId = reader.GetGuid(2);
                        model.Title = reader.GetString(3);
                        model.Descr = reader.GetString(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
