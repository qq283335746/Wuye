using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.WcfService
{
    public partial class CollectLifeService : ICollectLife
    {
        public static readonly string WebSiteHost = ConfigurationManager.AppSettings["WebSiteHost"].Trim('/');

        #region IAdvertisement Member

        public string GetSiteFunList()
        {
            try
            {
                ContentType ctBll = new ContentType();
                var dic = ctBll.GetKeyValueByParent(Enum.GetName(typeof(SysEnumHelper.ContentType), (int)SysEnumHelper.ContentType.AdvertisementFun));

                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                foreach (var kvp in dic)
                {
                    sb.Append("<N>");
                    sb.AppendFormat("<Id>{0}</Id><Name>{1}</Name>", kvp.Key, kvp.Value);
                    sb.Append("</N>");
                }
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public string GetAdvertisementList(int pageIndex, int pageSize, Guid siteFunId)
        {
            try
            {
                int totalRecords = 0;
                Advertisement bll = new Advertisement();
                AdvertisementLink adlBll = new AdvertisementLink();
                var list = bll.GetListByFunId(pageIndex, pageSize, out totalRecords, siteFunId);
                if (list == null || list.Count == 0) return "";

                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                foreach (var model in list)
                {
                    string adLinkAppend = "";
                    var adLinkList = adlBll.GetListByAdId(model.Id);
                    if (adLinkList != null && adLinkList.Count > 0)
                    {
                        adLinkAppend += "<AdImages>";

                        foreach (var adlModel in adLinkList)
                        {
                            Dictionary<string, string> dic = null;
                            if (!string.IsNullOrWhiteSpace(adlModel.FileDirectory) && !string.IsNullOrWhiteSpace(adlModel.FileExtension) && !string.IsNullOrWhiteSpace(adlModel.RandomFolder))
                            {
                                EnumData.Platform platform = EnumData.Platform.Android;
                                dic = PictureUrlHelper.GetUrlByPlatform(adlModel.FileDirectory, adlModel.RandomFolder, adlModel.FileExtension, platform);
                            }

                            adLinkAppend += "<AdImageInfo>";

                            adLinkAppend += string.Format(@"<ImageId>{0}</ImageId><AdId>{1}</AdId><ActionType>{2}</ActionType><Url><![CDATA[{3}]]></Url><Sort>{4}</Sort><OriginalPicture>{5}</OriginalPicture><BPicture>{6}</BPicture><MPicture>{7}</MPicture><SPicture>{8}</SPicture>",
                                                          adlModel.Id, adlModel.AdvertisementId, adlModel.ActionTypeName, adlModel.Url, adlModel.Sort, string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["OriginalPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["BPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["MPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["SPicture"]));

                            adLinkAppend += "</AdImageInfo>";
                        }
                        adLinkAppend += "</AdImages>";
                    }

                    sb.Append("<AdRes>");
                    sb.AppendFormat("<Id>{0}</Id><Title><![CDATA[{1}]]></Title><SiteFun>{2}</SiteFun><LayoutPosition>{3}</LayoutPosition><Duration>{4}</Duration><AdLink>{5}</AdLink>", model.Id, model.Title, model.SiteFunName, model.LayoutPositionName, model.Timeout, adLinkAppend);
                    sb.Append("</AdRes>");
                }
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public string GetAdvertisementModel(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty)) return "";

                Advertisement bll = new Advertisement();
                var model = bll.GetModelByJoin(Id);
                if (model == null) return "";

                Regex r = new Regex("(<img)(.*)src=\"([^\"]*?)\"(.*)/>");

                AdvertisementLink adlBll = new AdvertisementLink();
                string adLinkAppend = "";
                var adLinkList = adlBll.GetListByAdId(model.Id);
                if (adLinkList != null && adLinkList.Count > 0)
                {
                    adLinkAppend += "<AdImages>";

                    foreach (var adlModel in adLinkList)
                    {
                        Dictionary<string, string> dic = null;
                        if (!string.IsNullOrWhiteSpace(adlModel.FileDirectory) && !string.IsNullOrWhiteSpace(adlModel.FileExtension) && !string.IsNullOrWhiteSpace(adlModel.RandomFolder))
                        {
                            EnumData.Platform platform = EnumData.Platform.Android;
                            dic = PictureUrlHelper.GetUrlByPlatform(adlModel.FileDirectory, adlModel.RandomFolder, adlModel.FileExtension, platform);
                        }

                        adLinkAppend += "<AdImageInfo>";
                        adLinkAppend += string.Format(@"<ImageId>{0}</ImageId><AdId>{1}</AdId><ActionType>{2}</ActionType><Url><![CDATA[{3}]]></Url><Sort>{4}</Sort><OriginalPicture>{5}</OriginalPicture><BPicture>{6}</BPicture><MPicture>{7}</MPicture><SPicture>{8}</SPicture>",
                                                      adlModel.Id, adlModel.AdvertisementId, adlModel.ActionTypeName, adlModel.Url, adlModel.Sort, string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["OriginalPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["BPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["MPicture"]), string.Format("{0}{1}", WebSiteHost, dic == null ? "" : dic["SPicture"]));
                        adLinkAppend += "</AdImageInfo>";
                    }
                    adLinkAppend += "</AdImages>";
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                sb.AppendFormat("<Id>{0}</Id><Title><![CDATA[{1}]]></Title><SiteFun>{2}</SiteFun><LayoutPosition>{3}</LayoutPosition><AdTime>{4}</AdTime><AdLink>{5}</AdLink><Descr>{6}</Descr><Content><![CDATA[{7}]]></Content>", model.Id, model.Title, model.SiteFunName, model.LayoutPositionName, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), adLinkAppend, model.Descr, r.Replace(model.ContentText, "$1$2src=\"" + WebSiteHost + "$3\" />"));
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region IAnnouncement Member

        public string GetAnnouncementList(int pageIndex, int pageSize)
        {
            try
            {
                Announcement bll = new Announcement();
                var list = bll.GetList(pageIndex, pageSize, string.Empty, null);
                if (list == null || list.Count == 0) return "";
                StringBuilder sb = new StringBuilder();
                sb.Append("<PublicNoticeList>");
                foreach (var model in list)
                {
                    sb.Append("<PublicNoticeInfo>");
                    sb.AppendFormat("<Id>{0}</Id><Title>{1}</Title><AdTime>{2}</AdTime><Descr>{3}</Descr>", model.Id, model.Title, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), model.Descr);
                    sb.Append("</PublicNoticeInfo>");
                }
                sb.Append("</PublicNoticeList>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public string GetAnnouncementModel(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty)) return "";

                Announcement bll = new Announcement();
                var model = bll.GetModel(Id);
                if (model == null) return "";

                Regex r = new Regex("(<img)(.*)src=\"([^\"]*?)\"(.*)/>");

                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                sb.AppendFormat("<Id>{0}</Id><Title>{1}</Title><AdTime>{2}</AdTime><Descr>{3}</Descr><Content><![CDATA[{4}]]></Content>", model.Id, model.Title, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), model.Descr, r.Replace(model.ContentText, "$1$2src=\"" + WebSiteHost + "$3\" />"));
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region INotice Member

        public string GetNoticeList(int pageIndex, int pageSize, string username)
        {
            try
            {
                WebSecurityService wsClient = new WebSecurityService();
                object userId = wsClient.GetUserId(username);
                if (userId == null) return "";
                int totalRecords = 0;
                Notice bll = new Notice();
                var list = bll.GetMyNotice(pageIndex, pageSize, out totalRecords, userId);
                if (list == null || list.Count == 0) return "";
                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                foreach (var model in list)
                {
                    sb.Append("<N>");
                    sb.AppendFormat("<Id>{0}</Id><Title>{1}</Title><AdTime>{2}</AdTime><Descr>{3}</Descr>", model.Id, model.Title, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), model.Descr);
                    sb.Append("</N>");
                }
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public string GetNoticeModel(Guid Id, string username)
        {
            try
            {
                if (Id.Equals(Guid.Empty)) return "";
                WebSecurityService wsClient = new WebSecurityService();
                object userId = wsClient.GetUserId(username);
                if (userId == null) return "";

                Notice bll = new Notice();
                var model = bll.GetMyNoticeModel(Id, userId);
                if (model == null) return "";

                Regex r = new Regex("(<img)(.*)src=\"([^\"]*?)\"(.*)/>");

                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                sb.AppendFormat("<Id>{0}</Id><Title>{1}</Title><AdTime>{2}</AdTime><Descr>{3}</Descr><Content><![CDATA[{4}]]></Content>", model.Id, model.Title, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), model.Descr, r.Replace(model.ContentText, "$1$2src=\"" + WebSiteHost + "$3\" />"));
                sb.Append("</Rsp>");

                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region IComplainRepair Member

        /// <summary>
        /// 保存公共投诉保修
        /// </summary>
        /// <param name="username"></param>
        /// <param name="phone"></param>
        /// <param name="descri"></param>
        /// <returns></returns>
        public string SavePublicComplainRepair(string username, string phone, string descri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phone)) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "联系号码为必填项");
                if (string.IsNullOrWhiteSpace(descri)) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "描述为必填项");

                WebSecurityService wsClient = new WebSecurityService();
                object userId = wsClient.GetUserId(username);
                if (userId == null) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "用户【" + username + "】不存在或已被删除");

                SysEnum seBll = new SysEnum();
                var seModel = seBll.GetModel(SysEnumHelper.GetEnumNameForComplainRepairType(SysEnumHelper.ComplainRepairType.PublicTerritory));
                if (seModel == null) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "找不到投诉保修类型，待系统管理员设定后再使用");

                ComplainRepairInfo model = new ComplainRepairInfo();
                model.UserId = Guid.Parse(userId.ToString());
                model.LastUpdatedDate = DateTime.Now;
                model.Phone = phone.Trim();
                model.Descri = descri.Trim();
                model.SysEnumId = Guid.Parse(seModel.Id.ToString());
                model.Status = (byte)SysEnumHelper.ComplainRepairStatus.受理中;
                model.Address = "";

                ComplainRepair bll = new ComplainRepair();
                int effect = bll.Insert(model);
                if (effect == 110) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "已存在相同数据记录，请勿重复提交！");
                if (effect < 1) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "操作失败，请正确操作！");
                else return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", true, "保存成功，我们会尽快处理！");
            }
            catch (Exception ex)
            {
                return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 保存企业投诉保修
        /// </summary>
        /// <param name="username"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="descri"></param>
        /// <returns></returns>
        public string SaveHouseOwnerComplainRepair(string username, string address, string phone, string descri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(address)) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "住址为必填项");
                if (string.IsNullOrWhiteSpace(phone)) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "联系号码为必填项");
                if (string.IsNullOrWhiteSpace(descri)) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "描述为必填项");

                WebSecurityService wsClient = new WebSecurityService();
                object userId = wsClient.GetUserId(username);
                if (userId == null) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "用户【" + username + "】不存在或已被删除");

                SysEnum seBll = new SysEnum();
                var seModel = seBll.GetModel(SysEnumHelper.GetEnumNameForComplainRepairType(SysEnumHelper.ComplainRepairType.ResidentFamily));
                if (seModel == null) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "找不到投诉保修类型，待系统管理员设定后再使用");

                ComplainRepairInfo model = new ComplainRepairInfo();
                model.UserId = Guid.Parse(userId.ToString());
                model.LastUpdatedDate = DateTime.Now;
                model.Phone = phone.Trim();
                model.Descri = descri.Trim();
                model.SysEnumId = Guid.Parse(seModel.Id.ToString());
                model.Status = (byte)SysEnumHelper.ComplainRepairStatus.受理中;
                model.Address = address.Trim();

                ComplainRepair bll = new ComplainRepair();
                int effect = bll.Insert(model);
                if (effect == 110) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "已存在相同数据记录，请勿重复提交！");
                if (effect < 1) return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "操作失败，请正确操作！");
                else return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", true, "保存成功，我们会尽快处理！");
            }
            catch (Exception ex)
            {
                return string.Format("<Rsp><IsOK>{0}</IsOK><RtMsg>{1}</RtMsg></Rsp>", false, "异常：" + ex.Message);
            }
        }

        public string GetComplainRepairList(int pageIndex, int pageSize, int repairType, string username)
        {
            string enumCode = SysEnumHelper.GetEnumNameForComplainRepairType(repairType);
            if (string.IsNullOrEmpty(enumCode)) return string.Empty;

            WebSecurityService wsClient = new WebSecurityService();
            object userId = wsClient.GetUserId(username);
            if (userId == null) return string.Empty;
            int totalRecords = 0;
            ComplainRepair bll = new ComplainRepair();
            var list = bll.GetListByUser(pageIndex, pageSize, out totalRecords, userId, enumCode);
            if (list == null || list.Count == 0) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("<Rsp>");
            foreach (var model in list)
            {
                sb.AppendFormat("<N><Id>{0}</Id><Phone>{1}</Phone><Descr>{2}</Descr><AdTime>{3}</AdTime><Status>{4}</Status><Address>{5}</Address></N>", model.Id, model.Phone, model.Descri, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), SysEnumHelper.GetEnumNameForComplainRepairStatus(model.Status), model.Address);
            }
            sb.Append("</Rsp>");

            return sb.ToString();
        }

        public string GetComplainRepairModel(Guid Id, string username)
        {
            WebSecurityService wsClient = new WebSecurityService();
            object userId = wsClient.GetUserId(username);
            if (userId == null) return "";

            ComplainRepair bll = new ComplainRepair();
            var model = bll.GetModel(Id);
            if (model == null) return string.Empty;

            return string.Format("<Rsp><N><Id>{0}</Id><Phone>{1}</Phone><Descr>{2}</Descr><AdTime>{3}</AdTime><Status>{4}</Status><Address>{5}</Address></N></Rsp>", model.Id, model.Phone, model.Descri, model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm"), SysEnumHelper.GetEnumNameForComplainRepairStatus(model.Status), model.Address);
        }

        #endregion

        
    }
}
