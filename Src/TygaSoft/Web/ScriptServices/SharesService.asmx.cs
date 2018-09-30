using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TygaSoft.CustomProvider;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.ScriptServices
{
    /// <summary>
    /// SharesService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class SharesService : System.Web.Services.WebService
    {
        HttpContext context = HttpContext.Current;

        #region 登录 注册

        //[WebMethod]
        //public string Login(string userName, string psw,string vc)
        //{
        //    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(psw) || string.IsNullOrWhiteSpace(vc))
        //    {
        //        return "用户名、密码、验证码为必填项";
        //    }

        //    userName = userName.Trim();
        //    psw = psw.Trim();
        //    vc = vc.Trim();
        //    Regex r = new Regex(@"^(\d+){5,15}$");
        //    if (!r.IsMatch(userName))
        //    {
        //        return "请输入正确的QQ号码";
        //    }

        //    r = new Regex(@"(([0-9]+)|([a-zA-Z]+)){6,30}");
        //    if (!r.IsMatch(psw))
        //    {
        //        return "密码正确格式由数字或字母组成的字符串，且最小6位，最大30位";
        //    }
        //    //r = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        //    //if (!r.IsMatch(email))
        //    //{
        //    //    return "请输入正确的电子邮箱格式";
        //    //}

        //    if (context.Request.Cookies["ValidateCode"] == null)
        //    {
        //        return "非法操作";
        //    }

        //    if (vc.ToLower() != context.Request.Cookies["ValidateCode"]["LoginVc"].ToLower())
        //    {
        //        return "验证码输入不正确，请检查！";
        //    }

        //    userName = userName.Trim();
        //    psw = psw.Trim();

        //    string userData = string.Empty;
        //    string errorMsg = string.Empty;
        //    try
        //    {
        //        MembershipUser user;
        //        if (!Membership.ValidateUser(userName, psw))
        //        {
        //            user = Membership.GetUser(userName);
        //            if (user == null)
        //            {
        //                return "用户名不存在";
        //            }
        //            if (user.IsLockedOut)
        //            {
        //                return "您的账号已被锁定，请联系管理员先解锁后才能登录！";
        //            }
        //            if (!user.IsApproved)
        //            {
        //                return "您的帐户尚未获得批准。您无法登录，直到管理员批准您的帐户！";
        //            }
        //            else
        //            {
        //                return "密码不正确，请检查！";
        //            }
        //        }
        //        user = Membership.GetUser(userName);

        //        userData = user.ProviderUserKey.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMsg = ex.Message;
        //    }

        //    if (!string.IsNullOrEmpty(errorMsg))
        //    {
        //        return errorMsg;
        //    }

        //    //登录成功，则

        //    bool isPersistent = true;
        //    //bool isRemember = true;
        //    //bool isAuto = false;
        //    double d = 180;
        //    //if (cbRememberMe.Checked) isAuto = true;
        //    //自动登录 设置时间为1年
        //    //if (isAuto) d = 525600;

        //    //创建票证
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(d),
        //        isPersistent, userData, FormsAuthentication.FormsCookiePath);
        //    //加密票证
        //    string encTicket = FormsAuthentication.Encrypt(ticket);
        //    //创建cookie
        //    context.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

        //    ////FormsAuthentication.RedirectFromLoginPage(userName, isPersistent);//使用此行会清空ticket中的userData ？！！！
        //    //context.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, isPersistent));

        //    return "1";
        //}

        //[WebMethod]
        //public string Register(string userName, string psw, string email, string sVc)
        //{
        //    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(psw) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(sVc))
        //    {
        //        return "带有*号的为必填项";
        //    }

        //    userName = userName.Trim();
        //    psw = psw.Trim();
        //    email = email.Trim();
        //    sVc = sVc.Trim();

        //    //Regex r = new Regex(@"^(\d+){5,15}$");
        //    //if (!r.IsMatch(userName))
        //    //{
        //    //    return "请输入正确的QQ号码";
        //    //}

        //    Regex r = new Regex(@"(([0-9]+)|([a-zA-Z]+)){6,30}");
        //    if (!r.IsMatch(psw))
        //    {
        //        return "密码正确格式由数字或字母组成的字符串，且最小6位，最大30位";
        //    }
        //    //r = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        //    //if (!r.IsMatch(email))
        //    //{
        //    //    return "请输入正确的电子邮箱格式";
        //    //}

        //    if (sVc.ToLower() != context.Request.Cookies["RegisterVc"].Value.ToLower())
        //    {
        //        return "验证码输入不正确，请检查！";
        //    }

        //    string errorMsg = string.Empty;
        //    try
        //    {
        //        MembershipCreateStatus status = MembershipCreateStatus.Success;
        //        MembershipUser user = Membership.CreateUser(userName, psw, email, "系统默认", "系统默认", false, out status);

        //        if (user == null)
        //        {
        //            return EnumMembershipCreateStatus.GetStatusMessage(status); ;
        //        }

        //        Task[] tasks = {
        //                           Task.Factory.StartNew(() => Roles.AddUserToRole(user.UserName, "Users")),
        //                           Task.Factory.StartNew(() => EmailForRegister(userName,user.CreationDate))
        //                       };

        //        Task.WaitAll(tasks);
                
        //    }
        //    catch (MembershipCreateUserException ex)
        //    {
        //        errorMsg = EnumMembershipCreateStatus.GetStatusMessage(ex.StatusCode);
        //    }
        //    catch (HttpException ex)
        //    {
        //        errorMsg = ex.Message;
        //    }
        //    if (!string.IsNullOrEmpty(errorMsg))
        //    {
        //        return errorMsg;
        //    }

        //    return "注册成功";
        //}

        [WebMethod]
        public string CheckUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return "-1";
            }

            try
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {
                    return "1";
                }
            }
            catch
            {
            }

            return "0";
        }

        #endregion
    }
}
