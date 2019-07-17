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
    public partial class HouseOwner
    {
        public string[] GetHouseOwnerByHouseId(object houseId)
        {
            return dal.GetHouseOwnerByHouseId(houseId);
        }

        public HouseOwnerInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public List<HouseOwnerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, string orderBy, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, orderBy, cmdParms);
        }
    }
}
