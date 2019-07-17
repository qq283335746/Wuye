using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class UserHouseOwnerInfo
    {
        public object UserId { get; set; }

        public Guid HouseOwnerId { get; set; }

        public string Password { get; set; }
    }
}
