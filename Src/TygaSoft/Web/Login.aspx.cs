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
using TygaSoft.SysHelper;

namespace TygaSoft.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.Cookies["UserInfo"] != null)
                    {
                        string userName = Request.Cookies["UserInfo"]["UserName"];
                        if (!string.IsNullOrWhiteSpace(userName))
                        {
                            AESEncrypt aes = new AESEncrypt();
                            ltrMyData.Text = "<div id=\"myData\" style=\"display:none;\">[{\"UserName\":\"" + aes.DecryptString(Server.HtmlEncode(userName)) + "\"}]</div>";
                        }
                    }
                }
                else
                {
                    OnLogin();
                }
            }
            catch
            {
            }
        }

        private void OnLogin()
        {
            string errorMsg = string.Empty;

            try
            {
                string userName = Request.Form["txtUserName"];
                string psw = Request.Form["txtPsw"];
                string sVc = Request.Form["txtVc"];

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(psw))
                {
                    MessageBox.Messager(this.Page, Page.Controls[0], "用户名或密码输入不能为空！", MessageContent.AlertTitle_Error, "error");
                    return;
                }

                if (string.IsNullOrWhiteSpace(sVc))
                {
                    MessageBox.Messager(this.Page, Page.Controls[0], "验证码输入不能为空！", MessageContent.AlertTitle_Error, "error");
                    return;
                }

                bool isRemember = Request.Form["cbRememberMe"] == "1" ? true : false;

                userName = userName.Trim();
                psw = psw.Trim();
                sVc = sVc.Trim();

                var cookie = Request.Cookies["LoginVc"];
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
                {
                    MessageBox.Messager(this.Page, Page.Controls[0], "验证码不存在或已过期！", MessageContent.AlertTitle_Error, "error");
                    return;
                }

                string validCode = cookie.Value;

                AESEncrypt aes = new AESEncrypt();

                if (sVc.ToLower() != aes.DecryptString(validCode).ToLower())
                {
                    MessageBox.Messager(this.Page, Page.Controls[0], "验证码输入不正确，请检查！", MessageContent.AlertTitle_Error, "error");
                    return;
                }

                string userData = string.Empty;


                MembershipUser userInfo = Membership.GetUser(userName);
                if (!Membership.ValidateUser(userName, psw))
                {
                    if (userInfo == null)
                    {
                        MessageBox.Messager(this.Page, Page.Controls[0], "用户名不存在！", MessageContent.AlertTitle_Sys_Info);
                        return;
                    }
                    if (userInfo.IsLockedOut)
                    {
                        MessageBox.Messager(this.Page, Page.Controls[0], "您的账号已被锁定，请联系管理员先解锁后才能登录！", MessageContent.AlertTitle_Sys_Info);
                        return;
                    }
                    if (!userInfo.IsApproved)
                    {
                        MessageBox.Messager(this.Page, Page.Controls[0], "您的帐户尚未获得批准。您无法登录，直到管理员批准您的帐户！", MessageContent.AlertTitle_Sys_Info);
                        return;
                    }
                    else
                    {
                        MessageBox.Messager(this.Page, Page.Controls[0], "密码不正确，请检查！", MessageContent.AlertTitle_Sys_Info);
                        return;
                    }
                }

                userData = userInfo.ProviderUserKey.ToString();

                //登录成功，则

                bool isPersistent = false;
                //bool isRemember = true;
                //bool isAuto = false;
                double d = 100;
                //if (cbRememberMe.Checked) isAuto = true;
                //自动登录 设置时间为7天
                //if (isAuto) d = 10080;

                //创建票证
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(d),
                    isPersistent, userData, FormsAuthentication.FormsCookiePath);
                //加密票证
                string encTicket = FormsAuthentication.Encrypt(ticket);
                //创建cookie
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                if (isRemember)
                {
                    Response.Cookies["UserInfo"]["UserName"] = aes.EncryptString(userName);
                    Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(7);
                }
                else
                {
                    if (Request.Cookies["UserInfo"] != null)
                    {
                        Response.Cookies["UserInfo"].Expires = DateTime.Now;
                    }
                }

                //FormsAuthentication.RedirectFromLoginPage(userName, isPersistent);//使用此行会清空ticket中的userData ？！！！
                Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, isPersistent));
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                MessageBox.Messager(this.Page, Page.Controls[0], errorMsg, MessageContent.AlertTitle_Sys_Info);
                return;
            }
        }
    }
}