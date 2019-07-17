using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
//using TygaSoft.ThreadProcessor;

namespace TygaSoft.ProcessorCA
{
    class Program
    {
        static void Main(string[] args)
        {
            //WeixinProcessor.Processor();

            string xml = "";
//            xml = @"
//                <ResponseMsg><RetCode>0</RetCode><RetMsg>成功</RetMsg>
//<RetData><Rsp><Id>af74bc62-0fb1-4594-9aba-cf15d809f215</Id><Title>私人定制</Title><SiteFun>homePage</SiteFun>
//<LayoutPosition>Layout-Top</LayoutPosition><AdTime>2015-08-12 22:04</AdTime><AdLink><AdImages><AdImageInfo><ImageId>65790a7c-ad2b-45c3-90b6-298f43c462de</ImageId><AdId>af74bc62-0fb1-4594-9aba-cf15d809f215</AdId>
//<ActionType>图文</ActionType><Url></Url><Sort>0</Sort><OriginalPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_11364883842/0812_11364883842.jpg</OriginalPicture>
//<BPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_11364883842/Android/0812_11364883842_0.jpg</BPicture>
//<MPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_11364883842/Android/0812_11364883842_1.jpg</MPicture>
//<SPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_11364883842/Android/0812_11364883842_2.jpg</SPicture>
//</AdImageInfo></AdImages></AdLink><Descr>汇生活平台</Descr><Content><![CDATA[]]>
//</Content></Rsp></RetData></ResponseMsg>
//
//            ";


            xml = @"
                <ResponseMsg>
<RetCode>0</RetCode><RetMsg>成功</RetMsg>
<RetData><Rsp>
<Id>a54446ac-43ac-4d4e-8918-ca8fb1019d7e</Id>
<Title>琼中中平镇特产直销</Title><SiteFun>homePage</SiteFun><LayoutPosition>Layout-Center</LayoutPosition><AdTime>2015-08-13 15:55</AdTime>
<AdLink>
<AdImages><AdImageInfo><ImageId>6c1f2ef9-b365-4c85-b04c-dcb69fb7a2f1</ImageId><AdId>a54446ac-43ac-4d4e-8918-ca8fb1019d7e</AdId>
<ActionType>跳转至外部网页</ActionType><Url><![CDATA[https://www.taobao.com/?tracelogww=ksqdl_tbsy&?tracelogw=wwapp_quanbu]]></Url><Sort>0</Sort>
<OriginalPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_12073838378/0812_12073838378.jpg</OriginalPicture>
<BPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_12073838378/Android/0812_12073838378_0.jpg</BPicture>
<MPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_12073838378/Android/0812_12073838378_1.jpg</MPicture>
<SPicture>http://m.tygaweb.com/FilesRoot/PictureProductSize/201508/0812_12073838378/Android/0812_12073838378_2.jpg</SPicture>
</AdImageInfo></AdImages></AdLink>
<Descr>琼中特产促销</Descr><Content>
<![CDATA[]]>
</Content>
</Rsp></RetData>
</ResponseMsg>
            ";

            var pxml = XElement.Parse(xml);

            Console.WriteLine("按任意键结束");
            Console.ReadLine();
        }
    }
}
