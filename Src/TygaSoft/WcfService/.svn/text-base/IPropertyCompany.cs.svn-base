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
        #region IPropertyCompany Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract(Name = "InsertPropertyCompany")]
        int InsertPropertyCompany(PropertyCompanyInfo model);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract(Name = "UpdatePropertyCompany")]
        int UpdatePropertyCompany(PropertyCompanyInfo model);

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeletePropertyCompany")]
        int DeletePropertyCompany(object Id);

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteBatchPropertyCompany")]
        bool DeleteBatchPropertyCompany(string json);

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPropertyCompanyModel")]
        PropertyCompanyInfo GetPropertyCompanyModel(object Id);

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPropertyCompanyListBy5")]
        string GetPropertyCompanyList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPropertyCompanyListBy4")]
        string GetPropertyCompanyList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPropertyCompanyListBy2")]
        string GetPropertyCompanyList(string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetPropertyCompanyList")]
        string GetPropertyCompanyList();

        #endregion
    }
}
