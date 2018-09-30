using System;
using System.Globalization;

namespace TygaSoft.WebHelper
{
    public class CustomsHelper
    {
        /// <summary>
        /// 根据时间创建字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateDateTimeString()
        {
            //确保产生的字符串唯一性，使用线程休眠
            System.Threading.Thread.Sleep(2);
            Random random = new System.Random(); ;
            return DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + random.Next(0, 9999).ToString().PadLeft(4, '0');
        }

        /// <summary>
        /// 根据时间创建字符串
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDateTime()
        {
            //确保产生的字符串唯一性，使用线程休眠
            System.Threading.Thread.Sleep(2);
            return DateTime.Now.ToString("yyyyMMdd_HHmmssffff", DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 获取当前数字对应的几等奖
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string GetTicketLevel(int n)
        {
            switch (n)
            {
                case 1:
                    return "一等奖";
                case 2:
                    return "二等奖";
                case 3:
                    return "三等奖";
                case 4:
                    return "四等奖";
                case 5:
                    return "五等奖";
                case 6:
                    return "六等奖";
                case 7:
                    return "七等奖";
                case 8:
                    return "八等奖";
                default:
                    return "";
            }
        }
    }
}
