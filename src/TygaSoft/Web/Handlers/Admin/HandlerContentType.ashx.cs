using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TygaSoft.BLL;
using TygaSoft.DBUtility;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerContentType 的摘要说明
    /// </summary>
    public class HandlerContentType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
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
                case "GetJsonForCombobox":
                    GetJsonForCombobox(context);
                    break;
                default:
                    break;
            }

        }

        private void GetJsonForCombobox(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string enumCode = context.Request.QueryString["enumCode"];

            ContentType seBll = new ContentType();
            var dic = seBll.GetKeyValueByParent(enumCode);
            if (dic == null || dic.Count() == 0)
            {
                context.Response.Write("[]");
            }

            string json = "";
            foreach (var kvp in dic)
            {
                json += "{\"id\":\"" + kvp.Key + "\",\"text\":\"" + kvp.Value + "\"},";
            }

            context.Response.Write("[" + json.Trim(',') + "]");
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