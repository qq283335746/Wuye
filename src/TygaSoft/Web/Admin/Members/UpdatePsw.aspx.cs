using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.DBUtility;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Admin.Members
{
    public partial class UpdatePsw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                OnSave();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void OnSave()
        {
            string oldPsw = txtOldPsw.Value.Trim();
            string newPsw = txtNewPsw.Value.Trim();

            string errorMsg = string.Empty;
            try
            {
                MembershipUser user = Membership.GetUser();
                if (user.ChangePassword(oldPsw, newPsw))
                {
                    MessageBox.MessagerShow(this.Page, Page.Controls[0], "修改密码成功！");
                }
                else
                {
                    MessageBox.Messager(this.Page, Page.Controls[0], "修改密码失败，请检查！", "系统提示");
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Messager(this.Page, Page.Controls[0], errorMsg, "系统提示");
            }
        }
    }
}