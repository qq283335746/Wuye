using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace TygaSoft.WebHelper
{
    public class WebCommon
    {
        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <returns></returns>
        public static Guid GetUserId()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        ///创建验证的票据
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] datas = userData.Split(',');
                        if (datas.Length >= 0)
                        {
                            return Guid.Parse(datas[0]);
                        }
                    }
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Guid GetUserId(HttpContext context)
        {
            if (context != null && context.User != null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (context.User.Identity is FormsIdentity)
                    {
                        ///创建验证的票据
                        FormsIdentity id = (FormsIdentity)context.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] datas = userData.Split(',');
                        if (datas.Length >= 0)
                        {
                            return Guid.Parse(datas[0]);
                        }
                    }
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 获取当前主机地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUrlHost(HttpContext context)
        {
            if (context != null && context.User != null)
            {
                int port = context.Request.Url.Port;
                string host = context.Request.Url.Host;
                if (port != 80) host += ":" + port.ToString();
                return "http://" + host + context.Request.ApplicationPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// 使用指定的应用程序路径将虚拟路径转换为应用程序绝对路径
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string ToVirtualAbsolutePath(string virtualPath) 
        {
            return VirtualPathUtility.ToAbsolute(virtualPath, HttpContext.Current.Request.ApplicationPath);
        }

        public const decimal POINTNUM = 1000;

        public const int PageIndex = 1;

        public const int PageSize = 20;

        public const int PageSize10 = 10;
    }
}
