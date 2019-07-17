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
    /// HandlerHouse 的摘要说明
    /// </summary>
    public class HandlerHouse : IHttpHandler
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
            if (!string.IsNullOrEmpty(context.Request.QueryString["houseCode"]))
            {
                sqlWhere = "and HouseCode like @HouseCode ";
                SqlParameter parm = new SqlParameter("@HouseCode", SqlDbType.VarChar, 50);
                parm.Value = "%" + context.Request.QueryString["HouseCode"].Trim() + "%";
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["unitId"]))
            {
                Guid buildingId = Guid.Parse(context.Request.QueryString["unitId"]);
                sqlWhere = "and ResidentialUnitId = @ResidentialUnitId ";
                SqlParameter parm = new SqlParameter("@ResidentialUnitId", SqlDbType.UniqueIdentifier);
                parm.Value = buildingId;
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }

            House bll = new House();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parms != null ? parms.ToArray() : null);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"HouseCode\":\"" + model.HouseCode + "\"},");
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