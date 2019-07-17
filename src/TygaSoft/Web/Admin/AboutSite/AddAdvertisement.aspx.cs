using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Admin.AboutSite
{
    public partial class AddAdvertisement : System.Web.UI.Page
    {
        Guid Id;
        string myDataAppend;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Guid.TryParse(Request.QueryString["Id"], out Id);
                }

                BindCbbActionType();
                Bind();

                ltrMyData.Text = "<div id=\"myDataAppend\" style=\"display:none;\">" + myDataAppend + "</div>";
            }
        }

        private void Bind()
        {
            if (!Id.Equals(Guid.Empty))
            {
                Page.Title = "编辑广告";

                Advertisement bll = new Advertisement();

                var model = bll.GetModelByJoin(Id);
                if (model != null)
                {
                    hId.Value = Id.ToString();
                    txtTitle.Value = model.Title;
                    txtTimeout.Value = model.Timeout.ToString();
                    txtaDescr.Value = model.Descr;
                    txtContent.Value = model.ContentText;

                    string imgContentPictureHtml = "";
                    AdvertisementLink alBll = new AdvertisementLink();
                    var picList = alBll.GetDsByAdId(Id);
                    if (picList != null && picList.Tables.Count > 0 && picList.Tables[0].Rows.Count > 0)
                    {
                        string adTemplateText = File.ReadAllText(Server.MapPath("~/Templates/PartialAdvertisement.txt"));

                        DataRowCollection drc = picList.Tables[0].Rows;
                        foreach (DataRow dr in drc)
                        {
                            string currTemplateText = adTemplateText;

                            string dir = dr["FileDirectory"] == null ? "" : dr["FileDirectory"].ToString().Trim();
                            string fileEx = dr["FileExtension"] == null ? "" : dr["FileExtension"].ToString().Trim();
                            string rndCode = dr["RandomFolder"] == null ? "" : dr["RandomFolder"].ToString().Trim();
                            string sMPicture = "";
                            if (!string.IsNullOrWhiteSpace(dir) && !string.IsNullOrWhiteSpace(fileEx) && !string.IsNullOrWhiteSpace(rndCode))
                            {
                                EnumData.PictureType picType = EnumData.PictureType.MPicture;
                                EnumData.Platform platform = EnumData.Platform.Android;
                                sMPicture = PictureUrlHelper.GetUrl(dir, rndCode, fileEx, picType, platform);
                            }

                            imgContentPictureHtml += string.Format(currTemplateText, sMPicture, dr["ContentPictureId"], dr["ActionTypeId"], dr["Url"], dr["Sort"], dr["IsDisable"], dr["Id"]);
                        }

                        ltrImgItem.Text = imgContentPictureHtml;
                    }

                    myDataAppend += "<div id=\"myDataForModel\">[{\"SiteFunId\":\"" + model.SiteFunId + "\",\"LayoutPositionId\":\"" + model.LayoutPositionId + "\"}]</div>";
                }
            }
        }

        /// <summary>
        /// 绑定广告作用类别
        /// </summary>
        private void BindCbbActionType()
        {
            string cbbJson = "";
            ContentType ctBll = new ContentType();
            var dic = ctBll.GetKeyValueByParent("AdvertisementCategory");
            foreach (var kvp in dic)
            {
                cbbJson += "{\"id\":\"" + kvp.Key + "\",\"text\":\"" + kvp.Value + "\"},";
            }
            cbbJson = "[" + cbbJson.Trim(',') + "]";

            myDataAppend += "<div id=\"myDataForActionType\">" + cbbJson + "</div>";
        }
    }
}