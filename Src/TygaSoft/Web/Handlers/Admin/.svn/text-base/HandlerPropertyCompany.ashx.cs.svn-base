using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TygaSoft.BLL;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerPropertyCompany 的摘要说明
    /// </summary>
    public class HandlerPropertyCompany : IHttpHandler
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
                case "GetJsonForDatagrid":
                    GetJsonForDatagrid(context);
                    break;
                default:
                    break;
            }
        }

        private void GetJsonForDatagrid(HttpContext context)
        {
            int totalRecords = 0; 
            int pageIndex = 1;
            int pageSize = 10;
            int.TryParse(context.Request.QueryString["page"], out pageIndex);
            int.TryParse(context.Request.QueryString["rows"], out pageSize);
            string sqlWhere = string.Empty;
            SqlParameter parm = null;
            if (!string.IsNullOrEmpty(context.Request.QueryString["companyName"]))
            {
                sqlWhere = "and CompanyName like @CompanyName ";
                parm = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 50);
                parm.Value = "%" + context.Request.QueryString["companyName"].Trim() + "%";
            }

            PropertyCompany bll = new PropertyCompany();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parm);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"CompanyName\":\"" + model.CompanyName + "\",\"ShortName\":\"" + model.ShortName + "\",\"ProvinceCityId\":\"" + model.ProvinceCityId + "\",\"Province\":\"" + model.Province + "\",\"City\":\"" + model.City + "\",\"District\":\"" + model.District + "\"},");
            }
            context.Response.Write("{\"total\":"+totalRecords+",\"rows\":["+sb.ToString().Trim(',')+"]}");
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