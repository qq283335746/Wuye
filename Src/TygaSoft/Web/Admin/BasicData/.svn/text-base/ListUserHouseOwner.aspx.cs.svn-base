using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;

namespace TygaSoft.Web.Admin.BasicData
{
    public partial class ListUserHouseOwner : System.Web.UI.Page
    {
        string myDataAppend;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        string queryStr;
        ParamsHelper parms;
        string sqlWhere;
        string houseOwnerName;
        string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["userName"]))
            {
                MessageBox.Messager(Page, "非法请求，原因：请指定一个用户再进行此操作", MessageContent.AlertTitle_Error, "error");
                return;
            }
            
            userName = Request.QueryString["userName"].Trim();
            lbUser.InnerText = userName;

            string errorMsg = string.Empty;
            try
            {
                if (!Page.IsPostBack)
                {
                    NameValueCollection nvc = Request.QueryString;
                    int index = 0;
                    foreach (string item in nvc.AllKeys)
                    {
                        GetParms(item, nvc);

                        if (item != "pageIndex" && item != "pageSize")
                        {
                            index++;
                            if (index > 1) queryStr += "&";
                            queryStr += string.Format("{0}={1}", item, Server.HtmlEncode(nvc[item]));
                        }
                    }

                    Bind();
                }

                ltrMyData.Text = "<div id=\"myDataAppend\" style=\"display:none;\">" + myDataAppend + "</div>";
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Messager(this.Page, MessageContent.GetString(MessageContent.Request_SysError, errorMsg), MessageContent.AlertTitle_Ex_Error, "error");
            }
        }

        private void Bind()
        {
            //查询条件
            GetSearchItem();

            List<HouseOwnerInfo> list = null;
            int totalRecords = 0;

            UserHouseOwner bll = new UserHouseOwner();
            if (parms != null && parms.Count() > 0)
            {
                list = bll.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, parms == null ? null : parms.ToArray());
            }
            else
            {
                list = bll.GetListByJoin(pageIndex, pageSize, out totalRecords, "", null);
            }

            rpData.DataSource = list;
            rpData.DataBind();

            myDataAppend += "<div id=\"myDataForPage\" style=\"display:none;\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>";
        }

        private void GetSearchItem()
        {
            parms = new ParamsHelper();

            sqlWhere += "and u.UserName = @UserName ";
            SqlParameter parm = new SqlParameter("@UserName", SqlDbType.NVarChar, 256);
            parm.Value = userName;
            parms.Add(parm);

            if (!string.IsNullOrEmpty(houseOwnerName))
            {
                sqlWhere += "and ho.HouseOwnerName like @HouseOwnerName ";
                parm = new SqlParameter("@HouseOwnerName", SqlDbType.NVarChar, 50);
                parm.Value = "%" + houseOwnerName + "%";

                parms.Add(parm);
            }
        }

        private void GetParms(string key, NameValueCollection nvc)
        {
            switch (key)
            {
                case "pageIndex":
                    Int32.TryParse(nvc[key], out pageIndex);
                    break;
                case "pageSize":
                    Int32.TryParse(nvc[key], out pageSize);
                    break;
                case "name":
                    txtName.Value = nvc[key];
                    houseOwnerName = nvc[key];
                    break;
                default:
                    break;
            }
        }
    }
}