using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using TygaSoft.Model;

namespace TygaSoft.WcfService
{
    public partial interface ICollectLife
    {
        #region IResidenceCommunity Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract(Name = "InsertResidenceCommunity")]
        int InsertResidenceCommunity(ResidenceCommunityInfo model);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract(Name = "UpdateResidenceCommunity")]
        int UpdateResidenceCommunity(ResidenceCommunityInfo model);

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteResidenceCommunity")]
        int DeleteResidenceCommunity(object Id);

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteBatchResidenceCommunity")]
        bool DeleteBatchResidenceCommunity(string json);

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetResidenceCommunityModel")]
        ResidenceCommunityInfo GetResidenceCommunityModel(object Id);

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetResidenceCommunityListBy5")]
        string GetResidenceCommunityList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetResidenceCommunityListBy4")]
        string GetResidenceCommunityList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetResidenceCommunityListBy2")]
        string GetResidenceCommunityList(string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetResidenceCommunityList")]
        string GetResidenceCommunityList();

        #endregion
    }
}
