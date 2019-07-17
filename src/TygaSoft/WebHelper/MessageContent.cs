using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.WebHelper
{
    public static class MessageContent
    {
        public static string GetString(string strString)
        {
            return strString;
        }
        public static string GetString(string strString, string param1)
        {
            return string.Format(strString, param1);
        }
        public static string GetString(string strString, string param1, string param2)
        {
            return string.Format(strString, param1, param2);
        }
        public static string GetString(string strString, string param1, string param2, string param3)
        {
            return string.Format(strString, param1, param2, param3);
        }

        public const string Submit_Success = "保存成功！";
        public const string Submit_InvalidError = "保存失败，请核对后再提交！";
        public const string Submit_Error = "保存失败，请联系管理员！";
        public const string Submit_Exist = "已存在相同数据记录，请勿重复提交！";
        public const string Submit_Params_InvalidError = "有“*”标识的为必填项，请检查！";
        public const string Submit_Params_InvalidExist = "当前{0}不存在，请核对后再提交！";
        public const string Submit_Params_InvalidEnough = "当前{0}不足，请核对后再提交！";
        public const string Submit_Params_GetInvalidRegex = "获取{0}的值格式不正确，请检查！";
        public const string Submit_Params_InvalidRegex = "{0}输入值格式不正确，请检查！";
        public const string Submit_InvalidRow = "请至少勾选一行进行操作！";
        public const string Submit_Ex_Params_InvalidExist = "系统异常，原因：当前{0}不存在，请联系管理员！";
        public const string Submit_Ex_Error = "系统异常，原因：{0}";

        public const string AlertTitle_Info = "温馨提醒";
        public const string AlertTitle_Error = "错误提示";
        public const string AlertTitle_Sys_Info = "系统提示";
        public const string AlertTitle_Ex_Error = "异常提示";

        public const string Role_InvalidError = "对不起，无权限，请联系管理员";

        public const string Request_InvalidError = "非法操作，已禁止执行！";
        public const string Request_InvalidStaffBasic = "请先完善人员基本信息！";
        public const string Request_InvalidArgument = "获取{0}的值为空字符串或格式不正确，请检查！";
        public const string Request_NotExist = "{0}不存在或已被删除！";
        public const string Request_InvalidCompareToPassword = "前后输入密码不一致！";
        public const string Request_SysError = "对不起，系统发生异常，原因：{0}";
    }
}
