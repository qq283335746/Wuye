using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class AdvertisementLink
    {
        public int DeleteByAdId(object advertisementId)
        {
            return dal.DeleteByAdId(advertisementId);
        }

        public void InsertBatch(DataTable dt)
        {
            dal.InsertBatch(dt);
        }

        public DataSet GetDsByAdId(object advertisementId)
        {
            string sqlWhere = "and adl.AdvertisementId = @AdvertisementId ";
            SqlParameter parm = new SqlParameter("@AdvertisementId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(advertisementId.ToString());
            return dal.GetDs(sqlWhere, parm);
        }

        public IList<AdvertisementLinkInfo> GetListByAdId(object advertisementId)
        {
            string sqlWhere = "and adl.AdvertisementId = @AdvertisementId ";
            SqlParameter parm = new SqlParameter("@AdvertisementId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(advertisementId.ToString());

            return dal.GetListByJoin(sqlWhere, parm);
        }
    }
}
