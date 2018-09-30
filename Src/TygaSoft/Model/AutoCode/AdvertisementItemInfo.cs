using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class AdvertisementItemInfo
    {
        public object AdvertisementId { get; set; }

        public string Descr { get; set; }

        public string ContentText { get; set; }
    }
}
