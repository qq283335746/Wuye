using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Web.Security;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.DBUtility;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerHouseOwner 的摘要说明
    /// </summary>
    public class HandlerHouseOwner : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
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
                    case "AddUserHouseOwner":
                        AddUserHouseOwner(context);
                        break;
                    case "DelUserHouseOwner":
                        DelUserHouseOwner(context);
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
            string sqlWhere = string.Empty;
            ParamsHelper parms = null;
            if (!string.IsNullOrEmpty(context.Request.QueryString["houseOwnerName"]))
            {
                sqlWhere = "and HouseOwnerName like @HouseOwnerName ";
                SqlParameter parm = new SqlParameter("@HouseOwnerName", SqlDbType.NVarChar, 50);
                parm.Value = "%" + context.Request.QueryString["houseOwnerName"].Trim() + "%";
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }
            if (!string.IsNullOrEmpty(context.Request.QueryString["houseId"]))
            {
                sqlWhere = "and HouseId = @HouseId ";
                SqlParameter parm = new SqlParameter("@HouseId", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(context.Request.QueryString["houseId"].Trim());
                if (parms == null) parms = new ParamsHelper();
                parms.Add(parm);
            }

            HouseOwner bll = new HouseOwner();
            var list = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parms != null ? parms.ToArray() : null);
            if (list == null || list.Count == 0)
            {
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var model in list)
            {
                sb.Append("{\"Id\":\"" + model.Id + "\",\"HouseOwnerName\":\"" + model.HouseOwnerName + "\",\"MobilePhone\":\"" + model.MobilePhone + "\",\"CompanyName\":\"" + model.CompanyName + "\",\"CommunityName\":\"" + model.CommunityName + "\",\"BuildingCode\":\"" + model.BuildingCode + "\",\"UnitCode\":\"" + model.UnitCode + "\",\"HouseCode\":\"" + model.HouseCode + "\"},");
            }
            context.Response.Write("{\"total\":" + totalRecords + ",\"rows\":[" + sb.ToString().Trim(',') + "]}");
        }

        private void AddUserHouseOwner(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Form["houseOwnerId"]))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数houseOwnerId值不能为空，请检查\"}");
                return;
            }
            Guid houseOwnerId = Guid.Empty;
            Guid.TryParse(context.Request.Form["houseOwnerId"], out houseOwnerId);
            if (houseOwnerId.Equals(Guid.Empty))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数houseOwnerId值无效，请检查\"}");
                return;
            }
            if (string.IsNullOrWhiteSpace(context.Request.Form["userName"]))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数userName值不能为空，请检查\"}");
                return;
            }
            MembershipUser user = Membership.GetUser(context.Request.Form["userName"]);
            if (user == null)
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数userName值无效，请检查\"}");
                return;
            }

            UserHouseOwnerInfo model = new UserHouseOwnerInfo();
            model.UserId = user.ProviderUserKey;
            model.HouseOwnerId = houseOwnerId;
            model.Password = "";

            UserHouseOwner bll = new UserHouseOwner();

            string message = "操作成功";
            if (bll.Insert(model) == 110) message = "已存在相同记录";

            context.Response.Write("{\"success\": true, \"message\": \"" + message + "！\"}");
            return;
        }

        private void DelUserHouseOwner(HttpContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Request.Form["houseOwnerId"]))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数houseOwnerId值不能为空，请检查\"}");
                return;
            }
            Guid houseOwnerId = Guid.Empty;
            Guid.TryParse(context.Request.Form["houseOwnerId"], out houseOwnerId);
            if (houseOwnerId.Equals(Guid.Empty))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数houseOwnerId值无效，请检查\"}");
                return;
            }
            if (string.IsNullOrWhiteSpace(context.Request.Form["userName"]))
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数userName值不能为空，请检查\"}");
                return;
            }
            MembershipUser user = Membership.GetUser(context.Request.Form["userName"]);
            if (user == null)
            {
                context.Response.Write("{\"success\": false, \"message\": \"参数userName值无效，请检查\"}");
                return;
            }

            UserHouseOwner bll = new UserHouseOwner();
            bll.Delete(user.ProviderUserKey, houseOwnerId);

            context.Response.Write("{\"success\": true, \"message\": \"操作成功！\"}");
            return;
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