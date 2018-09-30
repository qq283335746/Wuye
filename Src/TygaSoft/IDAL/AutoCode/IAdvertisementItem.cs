using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IAdvertisementItem
    {
        #region IAdvertisementItem Member

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Insert(AdvertisementItemInfo model);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(AdvertisementItemInfo model);

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <returns></returns>
        int Delete(object advertisementId);

        /// <summary>
        /// 批量删除数据（启用事务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteBatch(IList<object> list);

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <returns></returns>
        AdvertisementItemInfo GetModel(object advertisementId);

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        IList<AdvertisementItemInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        IList<AdvertisementItemInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        IList<AdvertisementItemInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        IList<AdvertisementItemInfo> GetList();

        #endregion
    }
}
