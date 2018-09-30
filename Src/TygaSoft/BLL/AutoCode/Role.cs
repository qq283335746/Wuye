using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public class Role
    {
        private static readonly IRole dal = DataAccess.CreateRole();

        #region 成员方法

        /// <summary>
        /// 更改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(RoleInfo model)
        {
            return dal.Update(model);
        }

        public List<RoleInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
