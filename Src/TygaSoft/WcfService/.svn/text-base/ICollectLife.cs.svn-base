using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "TygaSoft.Services.CollectLifeService")]
    public partial interface ICollectLife
    {
        #region IAdvertisement Member

        /// <summary>
        /// 获取广告区分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSiteFunList")]
        string GetSiteFunList();

        /// <summary>
        /// 获取广告分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="siteFunId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAdvertisementList")]
        string GetAdvertisementList(int pageIndex, int pageSize, Guid siteFunId);

        /// <summary>
        /// 获取当前广告详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAdvertisementModel")]
        string GetAdvertisementModel(Guid Id);

        #endregion

        #region IAnnouncement Member

        /// <summary>
        /// 获取公告分页数据列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetAnnouncementList")]
        string GetAnnouncementList(int pageIndex, int pageSize);

        /// <summary>
        /// 获取当前公告详情
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetAnnouncementModel")]
        string GetAnnouncementModel(Guid Id);

        #endregion

        #region INotice Member

        /// <summary>
        /// 获取通知分页数据列表
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetNoticeList")]
        string GetNoticeList(int pageIndex, int pageSize, string username);

        /// <summary>
        /// 获取当前通知详情
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetNoticeModel")]
        string GetNoticeModel(Guid Id, string username);

        #endregion

        #region IComplainRepair Member

        /// <summary>
        /// 保存公共投诉保修
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phone"></param>
        /// <param name="descri"></param>
        /// <returns></returns>
        [OperationContract(Name = "SavePublicComplainRepair")]
        string SavePublicComplainRepair(string username, string phone, string descri);

        /// <summary>
        /// 保存企业投诉保修
        /// </summary>
        /// <param name="username"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="descri"></param>
        /// <returns></returns>
        [OperationContract(Name = "SaveHouseOwnerComplainRepair")]
        string SaveHouseOwnerComplainRepair(string username, string address, string phone, string descri);

        /// <summary>
        /// 获取用户投诉保修分页数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="repairType"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetComplainRepairList")]
        string GetComplainRepairList(int pageIndex, int pageSize, int repairType, string username);

        /// <summary>
        /// 获取当前投诉保修详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetComplainRepairModel")]
        string GetComplainRepairModel(Guid Id, string username);

        #endregion

    }
}
