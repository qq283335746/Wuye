using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class UserInfo
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string CfmPsw { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }

        public bool IsApproved { get; set; }
    }
}
