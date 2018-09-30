using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ContentTypeInfo
    {
        public object Id { get; set; }

        public string TypeName { get; set; }

        public string TypeCode { get; set; }

        public string TypeValue { get; set; }

        public Guid ParentId { get; set; }

        public Int32 Sort { get; set; }

        public Boolean IsSys { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
