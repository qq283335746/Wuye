using System;
using System.Runtime.Serialization;

namespace TygaSoft.CustomExceptions
{
    [Serializable()]
    public class CustomException : Exception,ISerializable
    {
        public CustomException() { }

        public CustomException(string message)
            : base(message)
        {
            HnztcSysClient sysClient = new HnztcSysClient();
            TygaSoft.Services.HnztcSysService.SyslogInfo sysLogInfo = new Services.HnztcSysService.SyslogInfo();
            sysLogInfo.AppName = "汇生活物业";
            sysLogInfo.MethodName = string.Format("{0}", base.TargetSite == null ? "" : base.TargetSite.Name);
            sysLogInfo.Message = string.Format("{0}{1}{2}", message, base.Source,base.StackTrace);
            sysLogInfo.LastUpdatedDate = DateTime.Now;
            sysClient.InsertSysLog(sysLogInfo);
        }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
            HnztcSysClient sysClient = new HnztcSysClient();
            TygaSoft.Services.HnztcSysService.SyslogInfo sysLogInfo = new Services.HnztcSysService.SyslogInfo();
            sysLogInfo.AppName = "汇生活物业";
            sysLogInfo.MethodName = string.Format("{0}", base.TargetSite == null ? "" : base.TargetSite.Name);
            sysLogInfo.Message = string.Format("{0}{1}{2}", message, base.Source, base.StackTrace);
            sysLogInfo.LastUpdatedDate = DateTime.Now;
            sysClient.InsertSysLog(sysLogInfo);
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
