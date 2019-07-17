using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Web.Security;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "TygaSoft.Services.WebSecurityService")]
    public partial interface IWebSecurity
    {
        [OperationContract(Name = "Login")]
        string Login(string username, string password);

        [OperationContract(Name = "GetUserId")]
        object GetUserId(string username);

        [OperationContract(Name = "ValidateUser")]
        bool ValidateUser(string username, string password);

        [OperationContract(Name = "GetUserByUsername")]
        string GetUser(string username);

        [OperationContract(Name = "ChangePassword")]
        string ChangePassword(string username, string oldPassword, string newPassword);
    }
}
