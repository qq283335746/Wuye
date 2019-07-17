using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.IDAL;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class ProvinceCity : IProvinceCity
    {
        public ProvinceCityInfo GetModel(string name)
        {
            ProvinceCityInfo model = null;

            string cmdText = @"select top 1 Id,Named,Pinyin,FirstChar,ParentId,Sort,Remark,LastUpdatedDate 
			                   from ProvinceCity
							   where Named = @Named ";
            SqlParameter parm = new SqlParameter("@Named", SqlDbType.NVarChar,50);
            parm.Value = name;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new ProvinceCityInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.Pinyin = reader.GetString(2);
                        model.FirstChar = reader.GetString(3);
                        model.ParentId = reader.GetGuid(4);
                        model.Sort = reader.GetInt32(5);
                        model.Remark = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

    }
}
