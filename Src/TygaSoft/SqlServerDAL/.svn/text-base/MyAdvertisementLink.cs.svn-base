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
    public partial class AdvertisementLink
    {
        #region IAdvertisementLink Member

        public void InsertBatch(DataTable dt)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.SqlProviderConnString))
            {
                bulkCopy.DestinationTableName = "dbo.AdvertisementLink";
                bulkCopy.ColumnMappings.Add("Id", "Id");
                bulkCopy.ColumnMappings.Add("AdvertisementId", "AdvertisementId");
                bulkCopy.ColumnMappings.Add("ActionTypeId", "ActionTypeId");
                bulkCopy.ColumnMappings.Add("ContentPictureId", "ContentPictureId");
                bulkCopy.ColumnMappings.Add("Url", "Url");
                bulkCopy.ColumnMappings.Add("Sort", "Sort");
                bulkCopy.ColumnMappings.Add("IsDisable", "IsDisable");

                bulkCopy.WriteToServer(dt);
            }
        }

        public int DeleteByAdId(object advertisementId)
        {
            string cmdText = "delete from AdvertisementLink where AdvertisementId = @AdvertisementId";
            SqlParameter parm = new SqlParameter("@AdvertisementId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(advertisementId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm);
        }

        public DataSet GetDs(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select adl.Id,adl.AdvertisementId,adl.ActionTypeId,adl.ContentPictureId,adl.Url,adl.Sort,adl.IsDisable,
                              ct.TypeCode ActionTypeCode,ct.TypeValue ActionTypeName,
                              adp.FileExtension,adp.FileDirectory,adp.RandomFolder
                              from AdvertisementLink adl 
                              left join Picture_Advertisement adp on adp.Id = adl.ContentPictureId
                              left join ContentType ct on ct.Id = adl.ActionTypeId
                              ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.Append(" where 1=1 " + sqlWhere);
            sb.Append(" order by adl.Sort ");

            return SqlHelper.ExecuteDataset(SqlHelper.SqlProviderConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public IList<AdvertisementLinkInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select adl.Id,adl.AdvertisementId,adl.ActionTypeId,adl.ContentPictureId,adl.Url,adl.Sort,adl.IsDisable
                              ,ct.TypeValue
                              ,pa.FileExtension,pa.FileDirectory,pa.RandomFolder
                              from AdvertisementLink adl 
                              left join Picture_Advertisement pa on pa.Id = adl.ContentPictureId
                              left join ContentType ct on ct.Id = adl.ActionTypeId
                              ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            IList<AdvertisementLinkInfo> list = new List<AdvertisementLinkInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdvertisementLinkInfo model = new AdvertisementLinkInfo();
                        model.Id = reader.GetGuid(0);
                        model.AdvertisementId = reader.GetGuid(1);
                        model.ActionTypeId = reader.GetGuid(2);
                        model.ContentPictureId = reader.GetGuid(3);
                        model.Url = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsDisable = reader.GetBoolean(6);
                        model.ActionTypeName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.FileExtension = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.FileDirectory = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.RandomFolder = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
