using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.WcfService
{
    public partial class CollectLifeService 
    {
        #region IPropertyCompany Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertPropertyCompany(PropertyCompanyInfo model)
        {
            PropertyCompany bll = new PropertyCompany();
            return bll.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePropertyCompany(PropertyCompanyInfo model)
        {
            PropertyCompany bll = new PropertyCompany();
            return bll.Update(model);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeletePropertyCompany(object Id)
        {
            PropertyCompany bll = new PropertyCompany();
            return bll.Delete(Id);
        }

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool DeleteBatchPropertyCompany(string json)
        {
            PropertyCompany bll = new PropertyCompany();
            IList<object> list = (IList<object>)JsonConvert.DeserializeObject(json);
            return bll.DeleteBatch(list);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public PropertyCompanyInfo GetPropertyCompanyModel(object Id)
        {
            PropertyCompany bll = new PropertyCompany();
            return bll.GetModel(Id);
        }

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public string GetPropertyCompanyList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            PropertyCompany bll = new PropertyCompany();
            List<PropertyCompanyInfo> list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public string GetPropertyCompanyList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            PropertyCompany bll = new PropertyCompany();
            List<PropertyCompanyInfo> list = bll.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public string GetPropertyCompanyList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            PropertyCompany bll = new PropertyCompany();
            List<PropertyCompanyInfo> list = bll.GetList(sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public string GetPropertyCompanyList()
        {
            PropertyCompany bll = new PropertyCompany();
            List<PropertyCompanyInfo> list = bll.GetList();
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        #endregion
    }
}
