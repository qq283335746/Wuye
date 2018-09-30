using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class HouseInfo
    {
        public object Id { get; set; }

        public string HouseCode { get; set; }

        public Guid PropertyCompanyId { get; set; }

        public Guid ResidenceCommunityId { get; set; }

        public Guid ResidentialBuildingId { get; set; }

        public Guid ResidentialUnitId { get; set; }

        public Double HouseAcreage { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
