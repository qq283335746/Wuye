using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Configuration;

namespace TygaSoft.WebHelper
{
    public class SitemapHelper
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);

        public static IList<string> Roles { get; set; }

        public static string GetTreeJsonForMenu()
        {
            string key = "Sitemap_Menu";
            string data = (string)HttpRuntime.Cache[key];
            if (data == null)
            {
                data = GetTreeJson();
                HttpRuntime.Cache.Add(key, data, null, DateTime.Now.AddHours(12), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }

        private static string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            SiteMapNodeCollection nodes = SiteMap.RootNode.ChildNodes;
            jsonAppend.Append("[");
            int index = -1;
            foreach (SiteMapNode node in nodes)
            {
                if (!IsInRole(node.Roles)) continue;
                if (node.Description == "hide") continue;
                index++;
                if (index > 0)
                    jsonAppend.Append(",");
                jsonAppend.Append("{\"id\":\"" + node.Url + "\",\"text\":\"" + node.Title + "\",\"state\":\"open\"");
                CreateTreeJson(node, ref jsonAppend);
                jsonAppend.Append("}");
            }

            jsonAppend.Append("]");

            return jsonAppend.ToString();
        }

        private static void CreateTreeJson(SiteMapNode currNode, ref StringBuilder jsonAppend)
        {
            if (currNode.HasChildNodes)
            {
                jsonAppend.Append(",\"children\":");
                jsonAppend.Append("[");
                int temp = -1;
                foreach (SiteMapNode node in currNode.ChildNodes)
                {
                    if (!IsInRole(node.Roles)) continue;
                    if (node.Description == "hide") continue;
                    temp++;
                    if (temp > 0) jsonAppend.Append(",");
                    jsonAppend.Append("{\"id\":\"" + node.Url + "\",\"text\":\"" + node.Title + "\",\"state\":\"open\"");
                    CreateTreeJson(node, ref jsonAppend);

                    jsonAppend.Append("}");
                }
                jsonAppend.Append("]");
            }
        }

        private static bool IsInRole(IList roles)
        {
            if (roles == null || roles.Count == 0) return true;

            foreach (string item in roles)
            {
                if (Roles.Contains(item)) return true;
            }

            return false;
        }
    }
}
