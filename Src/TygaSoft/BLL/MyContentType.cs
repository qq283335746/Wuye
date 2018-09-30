using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class ContentType
    {
        /// <summary>
        /// 获取属于当前代号下的所有子节点
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public Dictionary<object, string> GetKeyValueByParent(string typeCode)
        {
            string sqlWhere = "and t2.TypeCode = @TypeCode ";
            SqlParameter parm = new SqlParameter("@TypeCode", SqlDbType.VarChar, 50);
            parm.Value = typeCode;

            return dal.GetKeyValue(sqlWhere, parm);
        }

        /// <summary>
        /// 获取对应数据
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        public ContentTypeInfo GetModelByTypeCode(string typeCode)
        {
            return dal.GetModelByTypeCode(typeCode);
        }

        /// <summary>
        /// 获取公告类别json格式字符串
        /// </summary>
        /// <returns></returns>
        public string GetTreeJsonForContentTypeByTypeCode(string typeCode)
        {
            ContentTypeInfo model = GetModelByTypeCode(typeCode);
            if (model == null) return "[]";
            SqlParameter parm = new SqlParameter("@ParentId", SqlDbType.UniqueIdentifier);
            parm.Value = model.Id;
            var list = GetList("and ParentId = @ParentId", parm);
            StringBuilder jsonAppend = new StringBuilder();

            if (list == null || list.Count == 0) return "[]";

            CreateTreeJson(list, model.Id, ref jsonAppend);

            return jsonAppend.ToString();
        }

        /// <summary>
        /// 获取所有类别json格式字符串
        /// </summary>
        /// <returns></returns>
        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetList();
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

        private void CreateTreeJson(List<ContentTypeInfo> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int temp = 0;
                foreach (var model in childList)
                {
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + model.TypeName + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + model.ParentId + "\",\"enumCode\":\"" + model.TypeCode + "\",\"enumValue\":\"" + model.TypeValue + "\",\"sort\":\"" + model.Sort + "\"}");
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
    }
}
