using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class RoleInfo
    {
        public object RoleId { get; set; }
        public string RoleName { get; set; }

        public string UserName { get; set; }
        public bool IsInRole { get; set; }
    }
}
