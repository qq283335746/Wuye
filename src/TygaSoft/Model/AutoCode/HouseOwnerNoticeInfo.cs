using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class HouseOwnerNoticeInfo
    {
        public object Id { get; set; }

        public Guid HouseOwnerId { get; set; }

        public Guid NoticeId { get; set; }

        public Byte Status { get; set; }

        public Boolean IsRead { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
