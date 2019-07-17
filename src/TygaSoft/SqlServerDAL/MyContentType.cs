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
    public partial class ContentType : IContentType
    {
        public Dictionary<object, string> GetKeyValue(string sqlWhere, params SqlParameter[] cmdParms)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();

            string cmdText = @"select t1.Id,t1.TypeValue 
                                from ContentType t1
                                join ContentType t2 on t2.Id = t1.ParentId ";

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                cmdText += " where 1=1 " + sqlWhere;
            }

            cmdText += " order by t1.Sort ";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dic.Add(reader[0], reader.GetString(1));

                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// 获取对应数据
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public ContentTypeInfo GetModelByTypeCode(string typeCode)
        {
            ContentTypeInfo model = null;

            string cmdText = @"select top 1 Id,TypeName,TypeCode,TypeValue,ParentId,Sort,IsSys,LastUpdatedDate 
			                   from ContentType
							   where TypeCode = @TypeCode ";
            SqlParameter parm = new SqlParameter("@TypeCode", SqlDbType.VarChar,50);
            parm.Value = typeCode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ContentTypeInfo();
                        model.Id = reader.GetGuid(0);
                        model.TypeName = reader.GetString(1);
                        model.TypeCode = reader.GetString(2);
                        model.TypeValue = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.IsSys = reader.GetBoolean(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }
    }
}
