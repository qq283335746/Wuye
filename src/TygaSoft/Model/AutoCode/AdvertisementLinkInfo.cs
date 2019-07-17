using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class AdvertisementLinkInfo
    {
        public object Id { get; set; }

        public Guid AdvertisementId { get; set; }

        public Guid ActionTypeId { get; set; }

        public Guid ContentPictureId { get; set; }

        public string Url { get; set; }

        public Int32 Sort { get; set; }

        public Boolean IsDisable { get; set; }
    }
}
