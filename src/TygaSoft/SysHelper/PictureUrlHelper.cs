using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class PictureUrlHelper
    {
        public static string GetMPicture(string dir, string rndCode, string fileExtension)
        {
            EnumData.Platform platform = EnumData.Platform.PC;
            return string.Format("{0}{1}/{2}/{1}_1{3}", dir, rndCode, platform.ToString(), fileExtension);
        }

        /// <summary>
        /// 以一个存储文件获取指定大中小图、适用平台的生成图片Url
        /// </summary>
        /// <param name="dir">上传图片存储的根目录</param>
        /// <param name="rndCode">随机数</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="picture">OriginalPicture, BPicture, MPicture, SPicture中的一种</param>
        /// <param name="platform">PC,Android,IOS中的一种</param>
        /// <returns></returns>
        public static string GetUrl(string dir, string rndCode, string fileExtension, EnumData.PictureType picture, EnumData.Platform platform)
        {
            switch ((byte)picture)
            { 
                case 0:
                    return string.Format("{0}{1}/{1}{2}", dir, rndCode, fileExtension);
                case 1:
                    return string.Format("{0}{1}/{2}/{1}_0{3}", dir, rndCode, platform.ToString(), fileExtension);
                case 2:
                    return string.Format("{0}{1}/{2}/{1}_1{3}", dir, rndCode, platform.ToString(), fileExtension);
                case 3:
                    return string.Format("{0}{1}/{2}/{1}_2{3}", dir, rndCode, platform.ToString(), fileExtension);
                default:
                    break;
            }

            return string.Empty;
        }

        /// <summary>
        /// 以一个存储文件获取适用于指定平台的生成图片Url（包括原图，大中小图）
        /// </summary>
        /// <param name="dir">上传图片存储的根目录</param>
        /// <param name="rndCode">随机数</param>
        /// <param name="fileExtension">文件扩展名</param>
        /// <param name="platform">平台（PC,Android,IOS）</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUrlByPlatform(string dir, string rndCode, string fileExtension, EnumData.Platform platform)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            Array picTypeValues = Enum.GetValues(typeof(EnumData.PictureType));
            foreach (byte item in picTypeValues)
            {
                switch (item)
                {
                    case 0:
                        dic.Add(Enum.GetName(typeof(EnumData.PictureType), item), GetUrl(dir, rndCode, fileExtension, EnumData.PictureType.OriginalPicture, platform));
                        break;
                    case 1:
                        dic.Add(Enum.GetName(typeof(EnumData.PictureType), item), GetUrl(dir, rndCode, fileExtension, EnumData.PictureType.BPicture, platform));
                        break;
                    case 2:
                        dic.Add(Enum.GetName(typeof(EnumData.PictureType), item), GetUrl(dir, rndCode, fileExtension, EnumData.PictureType.MPicture, platform));
                        break;
                    case 3:
                        dic.Add(Enum.GetName(typeof(EnumData.PictureType), item), GetUrl(dir, rndCode, fileExtension, EnumData.PictureType.SPicture, platform));
                        break;
                    default:
                        break;
                }
            }

            return dic;
        }
    }
}
