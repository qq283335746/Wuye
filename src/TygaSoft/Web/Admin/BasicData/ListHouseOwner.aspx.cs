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
    public partial class ListHouseOwner : System.Web.UI.Page
    {
        string myDataAppend;
        int pageIndex = WebCommon.PageIndex;
        int pageSize = WebCommon.PageSize10;
        string queryStr;
        ParamsHelper parms;
        string sqlWhere;
        string orderBy;
        string parentType;
        string keyword;

        protected void Page_Load(object sender, EventArgs e)
        {
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

            HouseOwner bll = new HouseOwner();
            list = bll.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, orderBy, parms == null ? null : parms.ToArray());

            rpData.DataSource = list;
            rpData.DataBind();

            myDataAppend += "<div id=\"myDataForPage\">[{\"PageIndex\":\"" + pageIndex + "\",\"PageSize\":\"" + pageSize + "\",\"TotalRecord\":\"" + totalRecords + "\",\"QueryStr\":\"" + queryStr + "\"}]</div>";
            myDataAppend += "<div id=\"myDataForSearch\">[{\"parentType\":\"" + parentType + "\",\"keyword\":\"" + keyword + "\"}]</div>";
        }

        private void GetSearchItem()
        {
            if (!string.IsNullOrWhiteSpace(parentType))
            {
                switch (parentType)
                {
                    case "0":
                        orderBy = "ho.PropertyCompanyId";
                        break;
                    case "1":
                        orderBy = "ho.ResidenceCommunityId";
                        break;
                    case "2":
                        orderBy = "ho.ResidentialBuildingId";
                        break;
                    case "3":
                        orderBy = "ho.ResidentialUnitId";
                        break;
                    case "4":
                        orderBy = "ho.HouseId";
                        break;
                    case "5":
                        orderBy = "ho.Id";
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (!string.IsNullOrWhiteSpace(parentType))
                {
                    #region 关键字在当前所属中查找

                    switch (parentType)
                    {
                        case "0":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and pc.CompanyName like @CompanyName ";
                            SqlParameter parm = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 30);
                            parm.Value = "%" + keyword + "%";

                            parms.Add(parm);

                            break;
                        case "1":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and rc.CommunityName like @CommunityName ";
                            SqlParameter parm1 = new SqlParameter("@CommunityName", SqlDbType.NVarChar, 30);
                            parm1.Value = "%" + keyword + "%";

                            parms.Add(parm1);
                            break;
                        case "2":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and rb.BuildingCode like @BuildingCode ";
                            SqlParameter parm2 = new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 20);
                            parm2.Value = "%" + keyword + "%";

                            parms.Add(parm2);
                            break;
                        case "3":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and ru.UnitCode like @UnitCode ";
                            SqlParameter parm3 = new SqlParameter("@UnitCode", SqlDbType.VarChar, 30);
                            parm3.Value = "%" + keyword + "%";

                            parms.Add(parm3);
                            break;
                        case "4":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and h.HouseCode like @HouseCode ";
                            SqlParameter parm4 = new SqlParameter("@HouseCode", SqlDbType.VarChar, 20);
                            parm4.Value = "%" + keyword + "%";

                            parms.Add(parm4);
                            break;
                        case "5":
                            if (parms == null) parms = new ParamsHelper();

                            sqlWhere += "and ho.HouseOwnerName like @HouseOwnerName ";
                            SqlParameter parm5 = new SqlParameter("@HouseOwnerName", SqlDbType.NVarChar, 30);
                            parm5.Value = "%" + keyword + "%";

                            parms.Add(parm5);
                            break;
                        default:
                            break;
                    }

                    #endregion
                }
                else
                {
                    sqlWhere += "and (pc.CompanyName like @CompanyName or rc.CommunityName like @CommunityName or rb.BuildingCode like @BuildingCode or ru.UnitCode like @UnitCode or h.HouseCode like @HouseCode or ho.HouseOwnerName like @HouseOwnerName)";
                    if (parms == null) parms = new ParamsHelper();
                    SqlParameter parm = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 30);
                    parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                    parm = new SqlParameter("@CommunityName", SqlDbType.NVarChar, 30);
                    parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                    parm = new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 20);
                    parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                    parm = new SqlParameter("@UnitCode", SqlDbType.VarChar, 30);
                    parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                    parm = new SqlParameter("@HouseCode", SqlDbType.VarChar, 20);
                    parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                    parm = new SqlParameter("@HouseOwnerName", SqlDbType.NVarChar, 30);
                    parm.Value = "%" + keyword + "%";
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
                case "parentType":
                    parentType = nvc[key];
                    break;
                case "keyword":
                    keyword = nvc[key];
                    break;
                default:
                    break;
            }
        }
    }
}