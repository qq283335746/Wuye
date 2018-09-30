using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.IDAL;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public class SysEnum
    {
        private static readonly ISysEnum dal = DataAccess.CreateSysEnum();

        #region 成员方法

        /// <summary>
        /// 添加数据到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(SysEnumInfo model)
        {
            return dal.Insert(model);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(SysEnumInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除对应数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(object Id)
        {
            return dal.Delete(Id);
        }

        /// <summary>
        /// 批量删除数据（启用事务）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SysEnumInfo GetModel(object Id)
        {
            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获取对应的数据
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public SysEnumInfo GetModel(string enumCode)
        {
            return dal.GetModel(enumCode);
        }

        /// <summary>
        /// 获取数据分页列表，并返回所有记录数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public List<SysEnumInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        /// <summary>
        /// 获取满足当前条件的数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public List<SysEnumInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public List<SysEnumInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<SysEnumInfo> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 获取包含当前枚举代号下的所有子节点项数据列表
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public List<SysEnumInfo> GetListIncludeChild(string enumCode)
        {
            List<SysEnumInfo> list = new List<SysEnumInfo>();
            var allList = GetList();
            var currModel = allList.Find(m => m.EnumCode == enumCode);
            if (currModel == null) return list;
            GetListIncludeChild(allList, currModel.Id, ref list);

            return list;
        }

        /// <summary>
        /// 获取所有枚举项json格式字符串
        /// </summary>
        /// <returns></returns>
        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            List<SysEnumInfo> list = GetList();
            if (list != null && list.Count > 0)
            {
                CreateTreeJson(list, Guid.Empty, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        /// <summary>
        /// 获取属于当前枚举代号的所有节点项json格式字符串
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public string GetTreeJsonForEnumCode(string enumCode)
        {
            StringBuilder jsonAppend = new StringBuilder();

            List<SysEnumInfo> list = GetList();
            var currModel = list.Find(m => m.EnumCode == enumCode);
            
            if (list != null && list.Count > 0)
            {
                jsonAppend.Append("[");
                jsonAppend.Append("{\"id\":\"" + currModel.Id + "\",\"text\":\"" + currModel.EnumValue + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + currModel.ParentId + "\",\"enumCode\":\"" + currModel.EnumCode + "\",\"enumValue\":\"" + currModel.EnumValue + "\",\"sort\":\"" + currModel.Sort + "\",\"remark\":\"" + currModel.Remark + "\"}");
                jsonAppend.Append(",\"children\":");
                CreateTreeJson(list, currModel.Id, ref jsonAppend);
                jsonAppend.Append("}]");
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeJson(List<SysEnumInfo> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int temp = 0;
                foreach (var model in childList)
                {
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + model.EnumName + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + model.ParentId + "\",\"enumCode\":\"" + model.EnumCode + "\",\"enumValue\":\"" + model.EnumValue + "\",\"sort\":\"" + model.Sort + "\",\"remark\":\"" + model.Remark + "\"}");
                    if (list.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(list, model.Id, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (temp < childList.Count - 1) jsonAppend.Append(",");
                    temp++;
                }
            }
            jsonAppend.Append("]");
        }

        private void GetListIncludeChild(List<SysEnumInfo> allList, object parentId, ref List<SysEnumInfo> list)
        {
            var childList = allList.FindAll(x => x.ParentId.Equals(parentId));
            if (childList != null && childList.Count > 0)
            {
                foreach (var model in childList)
                {
                    list.Add(model);
                    if (allList.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        GetListIncludeChild(allList, model.Id, ref list);
                    }
                }
            }
        }

        #endregion
    }
}
