using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Handlers.Admin
{
    /// <summary>
    /// HandlerKindeditor 的摘要说明
    /// </summary>
    public class HandlerKindeditor : IHttpHandler
    {
        HttpContext context = HttpContext.Current;
        string filesRoot = ConfigHelper.GetValueByKey("FilesRoot").TrimEnd('/');

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request.HttpMethod.ToLower();
            switch (method)
            {
                case "post":
                    KindeditorFilesUpload();
                    break;
                case "get":
                    KindeditorFilesManager();
                    break;
                default:
                    break;
            }
        }

        public void KindeditorFilesUpload()
        {
            HttpPostedFile file = context.Request.Files["imgFile"];
            if (file == null || file.ContentLength == 0)
            {
                UploadResult(false, "请选择文件!", "");
            }
            string dir = context.Request.QueryString["dir"];
            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "image";
            }

            try
            {
                if (!UploadFilesHelper.IsFileValidated(file.InputStream, file.ContentLength))
                {
                    UploadResult(false, "该文件已被禁止上传", "");
                }
                string fileExt = Path.GetExtension(file.FileName).ToLower();

                string virtualPath = string.Empty;
                string fullPath = GetFullPath(Path.Combine("Kindeditor",dir), fileExt, out virtualPath);

                file.SaveAs(fullPath);

                UploadResult(true, "", WebCommon.ToVirtualAbsolutePath(virtualPath));
            }
            catch (Exception ex)
            {
                UploadResult(false, "上传异常：" + ex.Message, "");
            }
        }

        private void UploadResult(bool isSuccess, string message, string url)
        {
            int isSuccessValue = isSuccess ? 0 : 1;
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash["error"] = isSuccessValue;
            if (isSuccess) hash["url"] = url;
            else hash["message"] = message;

            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(hash));
            context.ApplicationInstance.CompleteRequest();

            //return JsonMapper.ToJson(hash);
        }

        private string GetFullPath(string dir, string fileExt, out string virtualPath)
        {
            string fileName = CustomsHelper.GetFormatDateTime();
            string fullPath = filesRoot;
            if (!string.IsNullOrWhiteSpace(dir))
            {
                fullPath = Path.Combine(fullPath, dir);
            }
            fullPath = Path.Combine(fullPath, fileName.Substring(0, 6));

            virtualPath = WebCommon.ToVirtualAbsolutePath(Path.Combine(fullPath,fileName+fileExt));

            fullPath = context.Server.MapPath(fullPath);

            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

            fullPath = Path.Combine(fullPath,fileName + fileExt);

            return fullPath;
        }

        private void KindeditorFilesManager()
        {
            string dirName = context.Request.QueryString["dir"];
            if (string.IsNullOrWhiteSpace(dirName))
            {
                dirName = "image";
            }
            if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
            {
                context.Response.Write("无效的目录名。");
                context.Response.End();
            }
            string virtualPath = Path.Combine(filesRoot,"Kindeditor",dirName);
            string fullPath = context.Server.MapPath(virtualPath);
            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            string currentPath = "";
            string currentUrl = "";
            string currentDirPath = "";
            string moveupDirPath = "";

            //根据path参数，设置各路径和URL
            string path = context.Request.QueryString["path"];
            path = string.IsNullOrWhiteSpace(path) ? "" : path;
            if (path == "")
            {
                currentPath = fullPath;
                currentUrl = WebCommon.ToVirtualAbsolutePath(virtualPath);
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = Path.Combine(fullPath,path);
                currentUrl = WebCommon.ToVirtualAbsolutePath(Path.Combine(virtualPath,path));
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            string order = context.Request.QueryString["order"];
            order = string.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                context.Response.Write("禁止操作。");
                context.Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                context.Response.Write("参数无效。");
                context.Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                Hashtable hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "目录不存在。";
                hash["file_list"] = "";
                context.Response.Write(JsonConvert.SerializeObject(hash));
                context.Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }

            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(JsonConvert.SerializeObject(result));
            context.Response.End();
        }

        private void UploadResult(string json)
        {
            context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            context.Response.Write(json);
            context.ApplicationInstance.CompleteRequest();
        }

        #region 实现IComparer接口进行比较

        class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}