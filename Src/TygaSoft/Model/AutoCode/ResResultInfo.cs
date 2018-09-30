using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class ResResultInfo
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Data { get; set; }
    }
}
