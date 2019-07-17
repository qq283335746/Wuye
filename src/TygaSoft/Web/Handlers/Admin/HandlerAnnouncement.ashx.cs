using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerAnnouncement 的摘要说明
    /// </summary>
    public class HandlerAnnouncement : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string msg = "";
            try
            {
                string reqName = "";
                switch (context.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        reqName = context.Request.QueryString["reqName"];
                        break;
                    case "POST":
                        reqName = context.Request.Form["reqName"];
                        break;
                    default:
                        break;
                }

                switch (reqName)
                {
                    case "SaveAnnouncement":
                        SaveAnnouncement(context);
                        break;
                    case "SaveNotice":
                        SaveNotice(context);
                        break;
                    case "SaveAdvertisement":
                        SaveAdvertisement(context);
                        break;
                    case "GetJsonForNoticeDatagrid":
                        GetJsonForNoticeDatagrid(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg != "")
            {
                context.Response.Write("{\"success\": false,\"message\": \"" + msg + "\"}");
            }

            //Stream stream = context.Request.InputStream;

            //long length = stream.Length;
            //byte[] data = context.Request.BinaryRead((int)length);//对当前输入流进行指定字节数的二进制读取 
            //string reqStr = System.Text.Encoding.UTF8.GetString(data);//解码为UTF8编码形式的字符串 

            //StreamReader steamRd = new StreamReader(stream);
            //string strPostData = steamRd.ReadToEnd();
            //string aa2 = HttpUtility.UrlDecode(strPostData, System.Text.Encoding.GetEncoding("utf-8"));

            //string aa = HttpUtility.UrlDecode(strPostData);

            //Stream stream = context.Request.InputStream;

            //StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            //string reqStr = HttpUtility.HtmlDecode(reader.ReadToEnd());
        }

        private void SaveAnnouncement(HttpContext context)
        {
            try
            {
                string Id = context.Request.Form["ctl00$cphMain$hId"].Trim();
                string title = context.Request.Form["ctl00$cphMain$txtTitle"].Trim();
                string sContentTypeId = context.Request.Form["contentTypeId"].Trim();
                string sDescr = context.Request.Form["ctl00$cphMain$txtaDescr"].Trim();
                string content = context.Request.Form["txtContent"].Trim();
                content = HttpUtility.HtmlDecode(content);
                Guid gId = Guid.Empty;
                if (Id != "") Guid.TryParse(Id, out gId);
                Guid contentTypeId = Guid.Empty;
                Guid.TryParse(sContentTypeId, out contentTypeId);

                AnnouncementInfo model = new AnnouncementInfo();
                model.LastUpdatedDate = DateTime.Now;
                model.Title = title;
                model.Descr = sDescr;
                model.ContentText = content;
                model.ContentTypeId = contentTypeId;
                model.Id = gId;

                Announcement bll = new Announcement();

                int effect = -1;
                if (!gId.Equals(Guid.Empty))
                {
                    effect = bll.Update(model);
                }
                else
                {
                    effect = bll.Insert(model);
                }

                if (effect != 1)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"操作失败，请正确输入\"}");
                    return;
                }

                context.Response.Write("{\"success\": true,\"message\": \"操作成功\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\": false,\"message\": \"异常：" + ex.Message + "\"}");
            }
        }

        private void SaveNotice(HttpContext context)
        {
            try
            {
                string Id = context.Request.Form["ctl00$cphMain$hId"].Trim();
                string sContentTypeId = context.Request.Form["contentTypeId"].Trim();
                string title = context.Request.Form["ctl00$cphMain$txtTitle"].Trim();
                string sDescr = context.Request.Form["ctl00$cphMain$txtaDescr"].Trim();
                string content = context.Request.Form["txtContent"].Trim();
                content = HttpUtility.HtmlDecode(content);
                Guid gId = Guid.Empty;
                if (Id != "") Guid.TryParse(Id, out gId);
                Guid contentTypeId = Guid.Empty;
                Guid.TryParse(sContentTypeId, out contentTypeId);

                NoticeInfo model = new NoticeInfo();
                model.LastUpdatedDate = DateTime.Now;
                model.Title = title;
                model.Descr = sDescr;
                model.ContentText = content;
                model.ContentTypeId = contentTypeId;
                model.Id = gId;

                Notice bll = new Notice();

                int effect = -1;
                if (!gId.Equals(Guid.Empty))
                {
                    effect = bll.Update(model);
                }
                else
                {
                    effect = bll.Insert(model);
                }

                if (effect != 1)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"操作失败，请正确输入\"}");
                    return;
                }

                context.Response.Write("{\"success\": true,\"message\": \"操作成功\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\": false,\"message\": \"异常：" + ex.Message + "\"}");
            }
        }

        private void SaveAdvertisement(HttpContext context)
        {
            try
            {
                string Id = context.Request.Form["ctl00$cphMain$hId"].Trim();
                string title = context.Request.Form["ctl00$cphMain$txtTitle"].Trim();
                string sSiteFunId = context.Request.Form["siteFunId"].Trim();
                string sLayoutPositionId = context.Request.Form["layoutPositionId"].Trim();
                string sTimeout = context.Request.Form["ctl00$cphMain$txtTimeout"].Trim();
                string sAdLinkJson = context.Request.Form["adLinkJson"].Trim();
                sAdLinkJson = sAdLinkJson.Trim(',');

                int timeout = 0;
                if (!string.IsNullOrWhiteSpace(sTimeout)) int.TryParse(sTimeout, out timeout);
                Guid siteFunId = Guid.Empty;
                Guid.TryParse(sSiteFunId, out siteFunId);
                Guid layoutPositionId = Guid.Empty;
                Guid.TryParse(sLayoutPositionId, out layoutPositionId);
                
                string sDescr = context.Request.Form["ctl00$cphMain$txtaDescr"].Trim();
                string content = context.Request.Form["content"].Trim();
                content = HttpUtility.HtmlDecode(content);

                Guid gId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(Id)) Guid.TryParse(Id, out gId);

                AdvertisementInfo model = new AdvertisementInfo();
                model.Id = gId;
                model.LastUpdatedDate = DateTime.Now;
                model.Title = title;
                model.Timeout = timeout;
                model.SiteFunId = siteFunId;
                model.LayoutPositionId = layoutPositionId;

                AdvertisementItemInfo adiModel = null;
                if (gId.Equals(Guid.Empty))
                {
                    if (!(string.IsNullOrWhiteSpace(sDescr) && string.IsNullOrWhiteSpace(content)))
                    {
                        adiModel = new AdvertisementItemInfo();
                        adiModel.Descr = sDescr;
                        adiModel.ContentText = content;
                    }
                }
                else
                {
                    adiModel = new AdvertisementItemInfo();
                    adiModel.Descr = sDescr;
                    adiModel.ContentText = content;
                    adiModel.AdvertisementId = gId;
                }

                IList<AdvertisementLinkInfo> listAdLink = null;

                if (!string.IsNullOrWhiteSpace(sAdLinkJson))
                {
                    DataTable dtPostData = JsonConvert.DeserializeObject<DataTable>(sAdLinkJson);
                    if (dtPostData != null && dtPostData.Rows.Count > 0)
                    {
                        listAdLink = new List<AdvertisementLinkInfo>();

                        foreach (DataRow dr in dtPostData.Rows)
                        {
                            AdvertisementLinkInfo postAdlModel = new AdvertisementLinkInfo();
                            postAdlModel.Id = dr["AdLinkId"] == DBNull.Value ? Guid.Empty : Guid.Parse(dr["AdLinkId"].ToString());
                            postAdlModel.AdvertisementId = gId;
                            postAdlModel.ContentPictureId = dr["ContentPictureId"] == DBNull.Value ? Guid.Empty : Guid.Parse(dr["ContentPictureId"].ToString());
                            postAdlModel.ActionTypeId = dr["ActionTypeId"] == DBNull.Value ? Guid.Empty : Guid.Parse(dr["ActionTypeId"].ToString());
                            postAdlModel.Url = dr["Url"] == DBNull.Value ? "" : dr["Url"].ToString();
                            postAdlModel.Sort = dr["Sort"] == DBNull.Value ? 0 : int.Parse(dr["Sort"].ToString());
                            postAdlModel.IsDisable = dr["IsDisable"] == DBNull.Value ? false : bool.Parse(dr["IsDisable"].ToString());

                            listAdLink.Add(postAdlModel);
                        }
                    }
                }

                Advertisement bll = new Advertisement();
                AdvertisementItem aiBll = new AdvertisementItem();
                AdvertisementLink adlBll = new AdvertisementLink();
                int effect = -1;
                List<AdvertisementLinkInfo> oldListAdLink = null;
                if (!gId.Equals(Guid.Empty)) oldListAdLink = adlBll.GetListByAdId(gId).ToList<AdvertisementLinkInfo>();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (!gId.Equals(Guid.Empty))
                    {
                        bll.Update(model);
                        aiBll.Update(adiModel);
                        if (listAdLink != null)
                        {
                            foreach (var adlModel in listAdLink)
                            {
                                if (oldListAdLink != null && oldListAdLink.Count > 0)
                                {
                                    var delModel = oldListAdLink.Find(m => m.Id.ToString() == adlModel.Id.ToString());
                                    if (delModel != null)
                                    {
                                        oldListAdLink.Remove(delModel);

                                        adlBll.Update(adlModel);
                                    }
                                    else
                                    {
                                        adlBll.Insert(adlModel);
                                    }
                                }
                                else
                                {
                                    adlBll.Insert(adlModel);
                                }
                            }
                        }

                        if (oldListAdLink != null && oldListAdLink.Count > 0)
                        {
                            IList<object> adlIdList = new List<object>();
                            foreach (var oldModel in oldListAdLink)
                            {
                                adlIdList.Add(oldModel.Id);
                            }
                            adlBll.DeleteBatch(adlIdList);
                        }

                        effect = 1;
                    }
                    else
                    {
                        Guid adId = Guid.NewGuid();
                        model.Id = adId;
                        effect = bll.Insert(model);

                        if (listAdLink != null && listAdLink.Count > 0)
                        {
                            foreach (var adlModel in listAdLink)
                            {
                                adlModel.AdvertisementId = adId;
                                adlBll.Insert(adlModel);
                            }
                        }

                        if (adiModel != null)
                        {
                            adiModel.AdvertisementId = adId;
                            aiBll.Insert(adiModel);
                        }
                    }

                    scope.Complete();
                }

                if (effect == 110)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"" + MessageContent.Submit_Exist + "\"}");
                    return;
                }
                if (effect < 1)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"操作失败，请正确输入\"}");
                    return;
                }

                context.Response.Write("{\"success\": true,\"message\": \"操作成功\"}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"success\": false,\"message\": \"异常：" + ex.Message + "\"}");
            }
        }

        private void GetJsonForNoticeDatagrid(HttpContext context)
        {
            int totalRecords = 0;
            int pageIndex = 1;
            int pageSize = 10;
            int.TryParse(context.Request.QueryString["page"], out pageIndex);
            int.TryParse(context.Request.QueryString["rows"], out pageSize);
            string sqlWhere = string.Empty;
            SqlParameter parm = null;
            if (!string.IsNullOrEmpty(context.Request.QueryString["title"]))
            {
                sqlWhere = "and Title like @Title ";
                parm = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
                parm.Value = "%" + context.Request.QueryString["title"].Trim() + "%";
            }

            Notice bll = new Notice();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parm);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"Title\":\"" + model.Title + "\"},");
            }
            context.Response.Write("{\"total\":" + totalRecords + ",\"rows\":[" + sb.ToString().Trim(',') + "]}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}