using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class ContentPicture
    {
        #region ContentPicture Member

        public IList<ContentPictureInfo> GetListInAppend(string pictureAppend)
        {
            string sqlWhere = "";
            string[] items = pictureAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                sqlWhere += string.Format("'{0}',", item);
            }
            sqlWhere = "and Id in (" + sqlWhere.Trim(',') + ")";

            return dal.GetList(sqlWhere, null);
        }

        #endregion
    }
}
