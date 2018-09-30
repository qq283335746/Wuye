using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Specialized;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;

namespace TygaSoft.Web.Admin.AboutSite
{
    public partial class ListAdvertisement : System.Web.UI.Page
    {
        string myDataAppend;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        string queryStr;
        ParamsHelper parms;
        StringBuilder sqlWhere;
        string keyword;

        protected void Page_Load(object sender, EventArgs e)
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

        private void Bind()
        {
            //查询条件
            GetSearchItem();

            int totalRecords = 0;
            Advertisement bll = new Advertisement();

            rpData.DataSource = bll.GetDs(pageIndex, pageSize, out totalRecords, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
            rpData.DataBind();

            myDataAppend += "<div id=\"myDataForPage\" style=\"display:none;\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>";
        }

        private void GetSearchItem()
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder();
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and (ad.Title like @Title or at.TypeCode like @Title or at.TypeCode like @Title or at.TypeName like @Title or at.TypeValue like @Title
                             or at1.TypeCode like @Title or at1.TypeName like @Title or at1.TypeValue like @Title
                             ) ");
                SqlParameter parm = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
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
                case "keyword":
                    txtName.Value = nvc[key];
                    keyword = nvc[key];
                    break;
                default:
                    break;
            }
        }
    }
}