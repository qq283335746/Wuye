using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IPictureAdvertisement
    {
        #region IPictureAdvertisement Member

        bool IsExist(string fileName, int size);

        #endregion
    }
}
