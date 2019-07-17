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

namespace TygaSoft.Web.Admin.AboutSite
{
    public partial class ListPictureAdvertisement : System.Web.UI.Page
    {
        string myDataAppend;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        string queryStr;
        ParamsHelper parms;
        string sqlWhere;
        DateTime startDate;
        DateTime endDate;

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

            myDataAppend += "<div id=\"myDataForReq\">[{\"startDate\":\"" + Request.QueryString["startDate"] + "\",\"endDate\":\"" + Request.QueryString["endDate"] + "\"}]</div>";

            ltrMyData.Text = "<div id=\"myDataAppend\" style=\"display:none;\">" + myDataAppend + "</div>";
        }

        private void Bind()
        {
            GetSearchItem();

            int totalRecords = 0;
            PictureAdvertisement bll = new PictureAdvertisement();

            rpData.DataSource = bll.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, parms == null ? null : parms.ToArray());
            rpData.DataBind();

            myDataAppend += "<div id=\"myDataForPage\" style=\"display:none;\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>";
        }

        private void GetSearchItem()
        {
            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                if (parms == null) parms = new ParamsHelper();

                sqlWhere += "and LastUpdatedDate between @StartDate and @EndDate ";
                SqlParameter parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                parm.Value = startDate;
                parms.Add(parm);
                parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                parm.Value = endDate;
                parms.Add(parm);
            }
            else
            {
                if (startDate != DateTime.MinValue)
                {
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere += "and LastUpdatedDate >= @StartDate ";
                    SqlParameter parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                }
                if (endDate != DateTime.MinValue)
                {
                    if (parms == null) parms = new ParamsHelper();
                    sqlWhere += "and LastUpdatedDate <= @EndDate ";
                    SqlParameter parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = endDate;
                    parms.Add(parm);
                }
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
                case "startDate":
                    if (!string.IsNullOrWhiteSpace(nvc[key]))
                    {
                        DateTime.TryParse(nvc[key], out startDate);
                    }
                    break;
                case "endDate":
                    if (!string.IsNullOrWhiteSpace(nvc[key]))
                    {
                        DateTime.TryParse(nvc[key], out endDate);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}