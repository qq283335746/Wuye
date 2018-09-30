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
    public partial class House
    {
        public string[] GetHouseByUnitId(object unitId)
        {
            return dal.GetHouseByUnitId(unitId);
        }

        public HouseInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public List<HouseInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }
    }
}
