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
        #region IProvinceCity Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertProvinceCity(ProvinceCityInfo model)
        {
            ProvinceCity bll = new ProvinceCity();
            return bll.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProvinceCity(ProvinceCityInfo model)
        {
            ProvinceCity bll = new ProvinceCity();
            return bll.Update(model);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteProvinceCity(object Id)
        {
            ProvinceCity bll = new ProvinceCity();
            return bll.Delete(Id);
        }

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public bool DeleteBatchProvinceCity(string json)
        {
            ProvinceCity bll = new ProvinceCity();
            IList<object> list = (IList<object>)JsonConvert.DeserializeObject(json);
            return bll.DeleteBatch(list);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetProvinceCityModel(object Id)
        {
            ProvinceCity bll = new ProvinceCity();
            ProvinceCityInfo model = bll.GetModel(Id);
            if (model == null) return "[]";
            return JsonConvert.SerializeObject(model);
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
        public string GetProvinceCityList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            ProvinceCity bll = new ProvinceCity();
            List<ProvinceCityInfo> list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
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
        public string GetProvinceCityList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            ProvinceCity bll = new ProvinceCity();
            List<ProvinceCityInfo> list = bll.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public string GetProvinceCityList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            ProvinceCity bll = new ProvinceCity();
            List<ProvinceCityInfo> list = bll.GetList(sqlWhere, cmdParms);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public string GetProvinceCityList()
        {
            ProvinceCity bll = new ProvinceCity();
            List<ProvinceCityInfo> list = bll.GetList();
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取所有省份列表
        /// </summary>
        /// <returns></returns>
        public string GetProvince()
        {
            ProvinceCity bll = new ProvinceCity();
            ProvinceCityInfo parentModel = bll.GetModel("中国");
            if (parentModel == null) return "[]";
            SqlParameter parm = new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier);
            parm.Value = parentModel.Id;
            List<ProvinceCityInfo> list = bll.GetList("and ParentId = @ParentId", parm);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取当前省份对应的市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public string GetCity(object provinceId)
        {
            ProvinceCity bll = new ProvinceCity();
            SqlParameter parm = new SqlParameter("@ParentId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(provinceId.ToString());
            List<ProvinceCityInfo> list = bll.GetList("and ParentId = @ParentId", parm);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取当前市的区列表
        /// </summary>
        /// <returns></returns>
        public string GetDistrict(object cityId)
        {
            ProvinceCity bll = new ProvinceCity();
            SqlParameter parm = new SqlParameter("@ParentId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(cityId.ToString());
            List<ProvinceCityInfo> list = bll.GetList("and ParentId = @ParentId", parm);
            if (list == null || list.Count == 0) return "[]";
            return JsonConvert.SerializeObject(list);
        }

        #endregion
    }
}
