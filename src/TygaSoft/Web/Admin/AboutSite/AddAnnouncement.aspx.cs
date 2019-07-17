using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.AboutSite
{
    public partial class AddAnnouncement : System.Web.UI.Page
    {
        Guid Id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Guid.TryParse(Request.QueryString["Id"], out Id);
                }

                Bind();
            }
        }

        private void Bind()
        {
            if (!Id.Equals(Guid.Empty))
            {
                Page.Title = "编辑公告";

                Announcement bll = new Announcement();
                var model = bll.GetModel(Id);
                if (model != null)
                {
                    txtTitle.Value = model.Title;
                    txtParent.Value = model.ContentTypeId.ToString();
                    txtaDescr.Value = model.Descr;
                    txtContent.Value = model.ContentText;
                    hId.Value = Id.ToString();
                }
            }
        }
    }
}