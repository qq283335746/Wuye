using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class SysEnumInfo
    {
        public object Id { get; set; }

        public string EnumName { get; set; }

        public string EnumCode { get; set; }

        public string EnumValue { get; set; }

        public object ParentId { get; set; }

        public int Sort { get; set; }

        public string Remark { get; set; }
    }
}
