using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ResidenceCommunityInfo
    {
        public object Id { get; set; }

        public string CommunityName { get; set; }

        public Guid PropertyCompanyId { get; set; }

        public Guid ProvinceCityId { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public string AboutDescri { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
