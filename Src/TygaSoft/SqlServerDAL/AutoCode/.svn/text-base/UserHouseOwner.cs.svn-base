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
    public partial class UserHouseOwner : IUserHouseOwner
    {
        #region 成员方法

        public int Insert(UserHouseOwnerInfo model)
        {
            if (IsExist(model.UserId, model.HouseOwnerId)) return 110;

            string cmdText = @"insert into UserHouseOwner (UserId,HouseOwnerId,Password)
			                 values
							 (@UserId,@HouseOwnerId,@Password)
			                 ";

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@Password",SqlDbType.VarChar,30)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.HouseOwnerId;
            parms[2].Value = model.Password;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Update(UserHouseOwnerInfo model)
        {
            string cmdText = @"update UserHouseOwner set Password = @Password 
			                 where UserId = @UserId and HouseOwnerId = @HouseOwnerId ";

            SqlParameter[] parms = {
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@HouseOwnerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Password",SqlDbType.VarChar,30)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.HouseOwnerId;
            parms[2].Value = model.Password;

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parms);
        }

        public int Delete(object UserId)
        {
            string cmdText = "delete from UserHouseOwner where UserId = @UserId";
            SqlParameter parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(UserId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            if (list == null || list.Count == 0) return false;

            bool result = false;
            StringBuilder sb = new StringBuilder();
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from UserHouseOwner where HouseOwnerId = @HouseOwnerId" + n + " ;");
                SqlParameter parm = new SqlParameter("@HouseOwnerId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.SqlProviderConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public UserHouseOwnerInfo GetModel(object UserId)
        {
            UserHouseOwnerInfo model = null;

            string cmdText = @"select top 1 UserId,HouseOwnerId,Password 
			                   from UserHouseOwner
							   where UserId = @UserId ";
            SqlParameter parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(UserId.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        model = new UserHouseOwnerInfo();
                        model.UserId = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);
                    }
                }
            }

            return model;
        }

        public List<UserHouseOwnerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select count(*) from UserHouseOwner ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms);

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          UserId,HouseOwnerId,Password
					  from UserHouseOwner ";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += "where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<UserHouseOwnerInfo> list = new List<UserHouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserHouseOwnerInfo model = new UserHouseOwnerInfo();
                        model.UserId = reader.GetGuid(1);
                        model.HouseOwnerId = reader.GetGuid(2);
                        model.Password = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<UserHouseOwnerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            string cmdText = @"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			                 UserId,HouseOwnerId,Password
							 from UserHouseOwner";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;
            cmdText += ")as objTable where RowNumber between " + startIndex + " and " + endIndex + " ";

            List<UserHouseOwnerInfo> list = new List<UserHouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserHouseOwnerInfo model = new UserHouseOwnerInfo();
                        model.UserId = reader.GetGuid(1);
                        model.HouseOwnerId = reader.GetGuid(2);
                        model.Password = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<UserHouseOwnerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            string cmdText = @"select UserId,HouseOwnerId,Password
                              from UserHouseOwner";
            if (!string.IsNullOrEmpty(sqlWhere)) cmdText += " where 1=1 " + sqlWhere;

            List<UserHouseOwnerInfo> list = new List<UserHouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText, cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserHouseOwnerInfo model = new UserHouseOwnerInfo();
                        model.UserId = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public List<UserHouseOwnerInfo> GetList()
        {
            string cmdText = @"select UserId,HouseOwnerId,Password 
			                from UserHouseOwner
							order by LastUpdatedDate desc ";

            List<UserHouseOwnerInfo> list = new List<UserHouseOwnerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlProviderConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserHouseOwnerInfo model = new UserHouseOwnerInfo();
                        model.UserId = reader.GetGuid(0);
                        model.HouseOwnerId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
