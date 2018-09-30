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
    /// HandlerResidenceCommunity 的摘要说明
    /// </summary>
    public class HandlerResidenceCommunity : IHttpHandler
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
            ParamsHelper parms = null;
            if (!string.IsNullOrEmpty(context.Request.QueryString["communityName"]))
            {
                sqlWhere = "and CommunityName like @CommunityName ";
                SqlParameter parm = new SqlParameter("@CommunityName", SqlDbType.NVarChar, 50);
                parm.Value = "%" + context.Request.QueryString["communityName"].Trim() + "%";
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["companyId"]))
            {
                Guid companyId = Guid.Parse(context.Request.QueryString["companyId"]);
                sqlWhere = "and PropertyCompanyId = @PropertyCompanyId ";
                SqlParameter parm = new SqlParameter("@PropertyCompanyId", SqlDbType.UniqueIdentifier);
                parm.Value = companyId;
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }

            ResidenceCommunity bll = new ResidenceCommunity();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parms != null ? parms.ToArray() : null);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"CommunityName\":\"" + model.CommunityName + "\",\"Address\":\"" + model.Address + "\"},");
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