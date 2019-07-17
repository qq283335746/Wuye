using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ContentDetailInfo
    {
        public object Id { get; set; }

        public Guid ContentTypeId { get; set; }

        public string Title { get; set; }

        public string ContentText { get; set; }

        public Int32 Sort { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
