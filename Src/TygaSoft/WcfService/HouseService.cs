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
        #region IHouse Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertHouse(HouseInfo model)
        {
            House bll = new House();
            return bll.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateHouse(HouseInfo model)
        {
            House bll = new House();
            return bll.Update(model);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteHouse(object Id)
        {
            House bll = new House();
            return bll.Delete(Id);
        }

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool DeleteBatchHouse(string json)
        {
            House bll = new House();
            IList<object> list = (IList<object>)JsonConvert.DeserializeObject(json);
            return bll.DeleteBatch(list);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public HouseInfo GetHouseModel(object Id)
        {
            House bll = new House();
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
        public string GetHouseList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            House bll = new House();
            List<HouseInfo> list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
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
        public string GetHouseList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            House bll = new House();
            List<HouseInfo> list = bll.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public string GetHouseList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            House bll = new House();
            List<HouseInfo> list = bll.GetList(sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public string GetHouseList()
        {
            House bll = new House();
            List<HouseInfo> list = bll.GetList();
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        #endregion
    }
}
