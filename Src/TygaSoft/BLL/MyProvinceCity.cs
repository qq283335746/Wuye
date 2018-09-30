using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class ProvinceCity
    {
        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ProvinceCityInfo GetModel(string name)
        {
            return dal.GetModel(name);
        }
    }
}
