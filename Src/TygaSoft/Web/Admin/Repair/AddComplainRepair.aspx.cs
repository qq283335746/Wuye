using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Admin.Repair
{
    public partial class AddComplainRepair : System.Web.UI.Page
    {
        Guid Id;
        string myDataAppend;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                Guid.TryParse(Request.QueryString["Id"], out Id);
            }

            if (!Page.IsPostBack)
            {
                string error = "";

                try
                {
                    string myDataForModelInfo = "";
                    Bind(ref myDataForModelInfo);
                    myDataAppend += "<div id=\"myDataForModelInfo\" style=\"display:none;\">[" + myDataForModelInfo + "]</div>";

                    ltrMyData.Text = "<div id=\"myDataAppend\" style=\"display:none;\">" + myDataAppend + "</div>";
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Messager(Page, error, MessageContent.AlertTitle_Ex_Error, "error");
                }
            }
        }

        private void Bind(ref string myDataAppend)
        {
            string error = "";
            try
            {
                if (!Id.Equals(Guid.Empty))
                {
                    Page.Title = "编辑物业公司信息";
                    ComplainRepair bll = new ComplainRepair();
                    ComplainRepairInfo model = bll.GetModel(Id);
                    if (model != null)
                    {
                        myDataAppend += "{ \"Id\":\"" + model.Id + "\",\"SysEnumId\":\"" + model.SysEnumId + "\",\"Phone\":\"" + model.Phone + "\",\"Address\":\"" + model.Address + "\",\"Descri\":\"" + model.Descri + "\"}";
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Messager(Page, error, MessageContent.AlertTitle_Ex_Error, "error");
            }
        }
    }
}