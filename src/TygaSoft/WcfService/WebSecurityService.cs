using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.CustomExceptions;

namespace TygaSoft.WcfService
{
    public partial class WebSecurityService:IWebSecurity
    {
        public string Login(string username, string password)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<LoginInfo>");
            string errorMsg = string.Empty;
            try
            {
                MembershipUser userInfo = Membership.GetUser(username);
                if (!Membership.ValidateUser(username, password))
                {
                    sb.AppendFormat("<IsLogined>{0}</IsLogined>", false);
                    if (userInfo == null)
                    {
                        sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", "用户名不存在！");
                    }
                    if (userInfo.IsLockedOut)
                    {
                        sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", "您的账号已被锁定，请联系管理员先解锁后才能登录！");
                    }
                    if (!userInfo.IsApproved)
                    {
                        sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", "您的帐户尚未获得批准。您无法登录，直到管理员批准您的帐户！");
                    }
                    else
                    {
                        sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", "密码不正确，请检查！");
                    }
                    sb.Append("<LoginData></LoginData>");
                }
                else
                {
                    sb.AppendFormat("<IsLogined>{0}</IsLogined>", true);
                    sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", "登录成功！");
                    sb.Append("<LoginData>");
                    sb.AppendFormat("<UserName>{1}</UserName><Address></Address><VipLevel></VipLevel><Mobile></Mobile>", userInfo.ProviderUserKey, userInfo.UserName);
                    sb.Append("</LoginData>");
                }
            }
            catch (Exception ex)
            {
                sb.AppendFormat("<IsLogined>{0}</IsLogined>", false);
                sb.AppendFormat("<LoginedRtMsg>{0}</LoginedRtMsg>", ex.Message);
                sb.Append("<LoginData></LoginData>");
            }

            sb.Append("</LoginInfo>");

            return sb.ToString();
        }

        public object GetUserId(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            if (user == null) return null;
            return user.ProviderUserKey;
        }

        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }

        public string GetUser(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            if (user == null) return "[]";
            return JsonConvert.SerializeObject(user);
        }

        public string ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                Regex r = new Regex(Membership.PasswordStrengthRegularExpression);
                if (!r.IsMatch(oldPassword) || !r.IsMatch(newPassword))
                {
                    sb.Append("<Rsp>");
                    sb.AppendFormat("<IsOk>{0}</IsOk>", false);
                    sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "密码必须是由数字或字母组成的字符串，且最小6位，最大30位！");
                    sb.Append("</Rsp>");
                    return sb.ToString();
                }

                MembershipUser user = Membership.GetUser(username);
                if (user == null)
                {
                    sb.Append("<Rsp>");
                    sb.AppendFormat("<IsOk>{0}</IsOk>", false);
                    sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "用户名不存在！");
                    sb.Append("</Rsp>");
                    return sb.ToString();
                }
                if (!Membership.ValidateUser(username, oldPassword))
                {
                    sb.Append("<Rsp>");
                    sb.AppendFormat("<IsOk>{0}</IsOk>", false);
                    if (user.IsLockedOut)
                    {
                        sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "您的账号已被锁定，请联系管理员先解锁后才能登录！");
                    }
                    else if (!user.IsApproved)
                    {
                        sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "您的帐户尚未获得批准。您无法登录，直到管理员批准您的帐户！");
                    }
                    else
                    {
                        sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "原密码输入不正确，请检查！");
                    }

                    sb.Append("</Rsp>");
                    return sb.ToString();
                }

                if (!user.ChangePassword(oldPassword, newPassword))
                {
                    sb.Append("<Rsp>");
                    sb.AppendFormat("<IsOk>{0}</IsOk>", false);
                    sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", "修改密码失败，请正确输入并重试");
                    sb.Append("</Rsp>");
                    return sb.ToString();
                }

                sb.Append("<Rsp>");
                sb.AppendFormat("<IsOk>{0}</IsOk>", true);
                sb.Append("<ErrorMsg></ErrorMsg>");
                sb.Append("</Rsp>");
                return sb.ToString();

            }
            catch (Exception ex)
            {
                new CustomException(string.Format("服务-接口：string ChangePassword(string username, string oldPassword, string newPassword)：异常：{0}", ex.Message), ex);
                StringBuilder sb = new StringBuilder();
                sb.Append("<Rsp>");
                sb.AppendFormat("<IsOk>{0}</IsOk>", false);
                sb.AppendFormat("<ErrorMsg>{0}</ErrorMsg>", ex.Message);
                sb.Append("</Rsp>");
                return sb.ToString();
            }
        }
    }
}
