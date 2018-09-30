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
    /// HandlerSysEnum 的摘要说明
    /// </summary>
    public class HandlerSysEnum : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

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
                case "GetJsonForCbbRepair":
                    GetJsonForCbbRepair(context);
                    break;
                default:
                    break;
            }

        }

        private void GetJsonForCbbRepair(HttpContext context)
        {
            SysEnum seBll = new SysEnum();
            var list = seBll.GetListIncludeChild("ComplainCategory");
            if (list == null || list.Count() == 0)
            {
                context.Response.Write("[]");
            }

            string json = "";
            foreach (var model in list)
            {
                json += "{\"id\":\""+model.Id+"\",\"text\":\""+model.EnumValue+"\"},";
            }

            context.Response.Write("["+json.Trim(',')+"]");
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