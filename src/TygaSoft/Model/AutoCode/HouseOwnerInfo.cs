using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class HouseOwnerInfo
    {
        public object Id { get; set; }

        public string HouseOwnerName { get; set; }

        public string MobilePhone { get; set; }

        public string TelPhone { get; set; }

        public Guid PropertyCompanyId { get; set; }

        public Guid ResidenceCommunityId { get; set; }

        public Guid ResidentialBuildingId { get; set; }

        public Guid ResidentialUnitId { get; set; }

        public Guid HouseId { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
