using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using TygaSoft.WcfService;

namespace TygaSoft.WcfCA
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost selfHost = new ServiceHost(typeof(CollectLifeService));

            try
            {
                selfHost.Open();
                Console.WriteLine("汇生活数据服务（绑定：basicHttpBinding，端口：18881）已启动就绪！");
                Console.WriteLine("终止服务请按任意键！");
                Console.WriteLine();
                Console.ReadLine();

                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("汇生活数据服务发生异常: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
