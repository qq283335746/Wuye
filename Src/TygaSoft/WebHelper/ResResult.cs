using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.WebHelper
{
    public class ResResult
    {
        public enum EnumCode { 成功 = 1000,失败 = 1001};

        public int ResCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
