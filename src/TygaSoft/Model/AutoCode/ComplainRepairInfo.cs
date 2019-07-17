using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ComplainRepairInfo
    {
        public object Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SysEnumId { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Descri { get; set; }

        public Byte Status { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
