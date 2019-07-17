using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PictureAdvertisementInfo
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public Int32 FileSize { get; set; }

        public string FileExtension { get; set; }

        public string FileDirectory { get; set; }

        public string RandomFolder { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
