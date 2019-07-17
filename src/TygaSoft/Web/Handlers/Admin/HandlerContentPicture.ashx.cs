using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerContentPicture 的摘要说明
    /// </summary>
    public class HandlerContentPicture : IHttpHandler
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
                    case "GetJsonForDatagrid":
                        GetJsonForDatagrid(context);
                        break;
                    case "OnUpload":
                        OnUpload(context);
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
        }

        private void GetJsonForDatagrid(HttpContext context)
        {
            int totalRecords = 0;
            int pageIndex = 1;
            int pageSize = 10;
            int.TryParse(context.Request.QueryString["page"], out pageIndex);
            int.TryParse(context.Request.QueryString["rows"], out pageSize);

            ContentPicture bll = new ContentPicture();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, "", null);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"OriginalPicture\":\"" + model.OriginalPicture.Replace("~", "") + "\",\"BPicture\":\"" + model.BPicture + "\",\"MPicture\":\"" + model.MPicture + "\",\"SPicture\":\"" + model.SPicture + "\",\"OtherPicture\":\"" + model.OtherPicture + "\",\"LastUpdatedDate\":\"" + model.LastUpdatedDate.ToString("yyyy-MM-dd HH:mm") + "\"},");
            }
            context.Response.Write("{\"total\":" + totalRecords + ",\"rows\":[" + sb.ToString().Trim(',') + "]}");
        }

        private void OnUpload(HttpContext context)
        {
            string errorMsg = "";
            try
            {
                int effect = 0;
                bool isCreateThumbnail = ConfigHelper.GetValueByKey("IsCteateContentPictureThumbnail") == "1";
                UploadFilesHelper ufh = new UploadFilesHelper();
                HttpFileCollection files = context.Request.Files;
                foreach (string item in files.AllKeys)
                {
                    if (files[item] == null || files[item].ContentLength == 0)
                    {
                        continue;
                    }

                    string[] paths = ufh.Upload(files[item], "ContentPictures", isCreateThumbnail);
                    ContentPicture ppBll = new ContentPicture();
                    ContentPictureInfo ppModel = new ContentPictureInfo();

                    string originalPicturePath = "";
                    string bPicturePath = "";
                    string mPicturePath = "";
                    string sPicturePath = "";
                    string otherPicturePath = "";
                    for (int i = 0; i < paths.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                originalPicturePath = paths[i].Replace("~", "");
                                break;
                            case 1:
                                bPicturePath = paths[i].Replace("~", "");
                                break;
                            case 2:
                                mPicturePath = paths[i].Replace("~", "");
                                break;
                            case 3:
                                sPicturePath = paths[i].Replace("~", "");
                                break;
                            case 4:
                                otherPicturePath = paths[i].Replace("~", "");
                                break;
                            default:
                                break;
                        }
                    }
                    ppModel.OriginalPicture = originalPicturePath;
                    ppModel.BPicture = bPicturePath;
                    ppModel.MPicture = mPicturePath;
                    ppModel.SPicture = sPicturePath;
                    ppModel.OtherPicture = otherPicturePath;
                    ppModel.LastUpdatedDate = DateTime.Now;
                    ppBll.Insert(ppModel);
                    effect++;
                }

                if (effect == 0)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"未找到任何可上传的文件，请检查！\"}");
                    return;
                }

                context.Response.Write("{\"success\": true,\"message\": \"已成功上传文件数：" + effect + "个\"}");
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            if (errorMsg != "")
            {
                context.Response.Write("{\"success\": false,\"message\": \"" + errorMsg + "\"}");
            }
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