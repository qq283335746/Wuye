using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace TygaSoft.SysHelper
{
    public class HttpHelper
    {
        /// <summary>
        /// Http Get 方法获取数据包
        /// </summary>
        /// <param name="url"></param>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        public static void DoHttpGet(string url, out int statusCode, out string result)
        {
            HttpWebRequest req = null;
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                req.Method = "Get";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream sw = res.GetResponseStream();
                StreamReader reader = new StreamReader(sw);
                result = reader.ReadToEnd();
                //获取响应结果的http状态码，200-请求成功
                statusCode = (int)res.StatusCode;
                res.Close();
                sw.Close();
                reader.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Http POST 方法发送数据包
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        public static void DoHttpPost(string url, string content, out int statusCode, out string result)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            statusCode = -1;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] data = encoding.GetBytes(content);

                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;

                Stream reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                res = (HttpWebResponse)req.GetResponse();
                Stream responseStream = res.GetResponseStream();

                var streamReader = new StreamReader(responseStream);
                result = streamReader.ReadToEnd();
                //获取响应结果的http状态码，200-请求成功
                statusCode = (int)res.StatusCode;
                res.Close();
                responseStream.Close();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }
    }
}
