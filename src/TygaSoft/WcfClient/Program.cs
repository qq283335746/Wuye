using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //SysLogClient slClient = new SysLogClient();
            //slClient.BeginInsertWeixinLog("WCFClient", "Program.Main","测试调用日志服务接口", null, null);

            //登录、人员信息等接口调用此实例
            //WebSecurityClient wsClient = new WebSecurityClient();
            //string sLogin = wsClient.Login("13687590736", "123456");
            //object sGetUserId = wsClient.GetUserId("18308909020"); //18308909020-8b4b1c0b-e588-4cf7-93d2-e3b79a60663b
            //Console.WriteLine("登录接口------------------------------------------");
            //Console.WriteLine(sLogin);

            //除登录、人员信息之外等接口调用此实例
            CollectLifeClient clClient = new CollectLifeClient();
            //Console.WriteLine("公告相关接口------------------------------------------");
            //string sGetAnnouncementList = clClient.GetAnnouncementList(1,10);      //获取公告分页数据列表
            //string sGetAnnouncementModel = clClient.GetAnnouncementModel(Guid.Parse("c24c9428-ca73-4db2-a524-9bea6e606e02"));    //获取公告详情
            //Console.WriteLine("通知相关接口------------------------------------------");
            //string sGetNoticeList = clClient.GetNoticeList(1, 10, "18308909020"); //<Rsp><N><Id>ce59654b-0c46-4e07-9fcd-82f630ff69a6</Id><Title>通知  没交电费的业主请尽快交</Title><AdTime>2015-06-07 21:41</AdTime><Descr></Descr></N><N><Id>9626c138-913f-49c5-a6bc-c946bb1881c1</Id><Title>通知 今晚12点停电</Title><AdTime>2015-06-07 21:40</AdTime><Descr>请各个业主做好接水准备</Descr></N></Rsp>
            //string sGetNoticeModel = clClient.GetNoticeModel(Guid.Parse("ce59654b-0c46-4e07-9fcd-82f630ff69a6"), "18308909020");

            Console.WriteLine("投诉保修相关接口------------------------------------------");
            //string sSavePublicComplainRepair = clClient.SavePublicComplainRepair("18308909020", "18308999999", "描述说明描述说明。。。");
            //string sSaveHouseOwnerComplainRepair = clClient.SaveHouseOwnerComplainRepair("18308909020", "海口市龙华区南站", "18308999999", "描述说明描述说明。。。");
            //string sGetComplainRepairList = clClient.GetComplainRepairList(1, 10, 0, "18308909020");
            //string sGetComplainRepairList2 = clClient.GetComplainRepairList(1, 10, 1, "18308909020");

            Console.WriteLine("广告相关接口------------------------------------------");
            var sGetSiteFunList = clClient.GetSiteFunList();    //获取广告区列表 
            var sGetAdvertisementList = clClient.GetAdvertisementList(1, 10, Guid.Parse("D45DC150-C566-46A7-8EA8-74CFB3D128ED"));    //获取当前广告区的所有广告
            var sGetAdvertisementModel = clClient.GetAdvertisementModel(Guid.Parse("FB62765C-2FE8-437A-8CC8-58DB88EB2EBB")); //D899A975-296D-4197-A2B5-7E873A375693 6fc7b533-5462-432f-aad3-90c53d6c4a2a


            Console.WriteLine("执行完毕！");

            Console.WriteLine("按任意键结束程序");
            Console.ReadLine();
        }
    }
}
