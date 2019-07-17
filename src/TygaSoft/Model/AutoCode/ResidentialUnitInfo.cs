using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ResidentialUnitInfo
    {
        public object Id { get; set; }

        public string UnitCode { get; set; }

        public Guid PropertyCompanyId { get; set; }

        public Guid ResidenceCommunityId { get; set; }

        public Guid ResidentialBuildingId { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
