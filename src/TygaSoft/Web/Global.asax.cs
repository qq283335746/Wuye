using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TygaSoft.Web
{
    public class Global : System.Web.HttpApplication
    {
        private const string DefaultUserName = "admin";
        private const string DefaultRoleName = "Administrators";

        protected void Application_Start(object sender, EventArgs e)
        {
            var roles = Roles.GetAllRoles();
            if (!roles.Any(m => m.Equals(DefaultRoleName, StringComparison.OrdinalIgnoreCase)))
            {
                Roles.CreateRole(DefaultRoleName);
            }

            if (!Membership.ValidateUser(DefaultUserName, DefaultUserName + "123456"))
            {
                var user = Membership.GetUser(DefaultUserName);
                if (user != null)
                {
                    Membership.DeleteUser(DefaultUserName, true);
                }

                user = Membership.CreateUser(DefaultUserName, DefaultUserName + "123456");
                Roles.AddUserToRole(DefaultUserName, DefaultRoleName);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}