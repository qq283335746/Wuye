<%@ Page Title="新建广告" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddAdvertisement.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.AddAdvertisement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <link href="/House/Scripts/plugins/kindeditor/themes/default/default.css" rel="stylesheet" type="text/css" />
  <link href="/House/Scripts/plugins/kindeditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" /> 
  <script src="/House/Scripts/JeasyuiExtend.js" type="text/javascript"></script>
  <script type="text/javascript" src="/House/Scripts/plugins/kindeditor/kindeditor.js"></script>
  <script type="text/javascript" src="/House/Scripts/plugins/kindeditor/lang/zh_CN.js"></script>
  <script type="text/javascript" src="/House/Scripts/plugins/kindeditor/plugins/code/prettify.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" style="padding:10px;">
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>标题：</span>
        <div class="fl" style="width:800px;">
            <input type="text" id="txtTitle" runat="server" clientidmode="Static" class="easyui-textbox" data-options="required:true,missingMessage:'必填项'" style="width:100%;" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>广告区：</span>
        <div class="fl">
            <input id="cbbSiteFun" class="easyui-combobox" data-options="required:true,missingMessage:'必选项',valueField:'id',textField:'text',url: '/House/Handlers/Admin/HandlerContentType.ashx?reqName=GetJsonForCombobox&enumCode=AdvertisementFun',method:'GET'" />
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>布局位置：</span>
        <div class="fl">
            <input id="cbbLayoutPosition" class="easyui-combobox" data-options="required:true,missingMessage:'必选项',valueField:'id',textField:'text',url: '/House/Handlers/Admin/HandlerContentType.ashx?reqName=GetJsonForCombobox&enumCode=LayoutPosition',method:'GET'" />
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="rl">上下架：</span>
        <div class="fl">
            <select id="cbbIsDisable" class="easyui-combobox" style="width:173px;">
                <option value="0">上架</option>
                <option value="1">下架</option>
            </select>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="rl">间隔时间：</span>
        <div class="fl">
            <input type="text" id="txtTimeout" runat="server" clientidmode="Static" class="easyui-textbox" data-options="validType:'float'" />
            （单位：秒）
        </div>
        <span class="clr"></span>
    </div>

    <div id="picBox" class="row mt10">
        <span class="rl">选择图片：</span>
        <div class="fl mr10">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="DlgPictureSelect.DlgMutil('PictureAdvertisement')">选 择</a>
        </div>
        <div class="fl" style="width:800px;">
            <div id="imgContentPicture">
                <div class="row_col w110 mb10" style="display:none; width:400px;">
                    <table style="width:100%;">
                        <tr>
                            <td rowspan="4" style="width:130px; vertical-align:top;">
                                <img src="" alt="" width="110px" height="110px" /><input type="hidden" name="PicId" />
                            </td>
                            <td style="width:70px;">
                              作用类型：  
                            </td>
                            <td>
                                <select name="ddlActionTypeId"></select>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>Url：</td>
                            <td>
                                <input type="text" name="Url" style="width:188px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>排序：</td>
                            <td>
                                <input type="text" name="Sort" style="width:80px;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="$(this).parents('.row_col').remove()">删 除</a>
                                <input type="checkbox" name="IsDisable" value="false" /> 不显示
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="AdLinkId" value="" />
                </div>
                <asp:Literal ID="ltrImgItem" runat="server"></asp:Literal>
            </div>
        </div>
        <span class="clr"></span>
    </div>

    <div id="descrBox" class="row mt10">
        <span class="rl">描述说明：</span>
        <div class="fl" style="width:791px;">
            <textarea id="txtaDescr" runat="server" clientidmode="Static" rows="3" cols="80" class="txta" style="width:100%;"></textarea>
        </div>
        <div class="clr"></div>
    </div>
    <div id="contentBox" class="row mt10">
        <span class="rl">内容：</span>
        <div class="fl">
            <textarea id="txtContent" runat="server" clientidmode="Static" cols="100" rows="8" style="width:800px;height:800px;"></textarea>
        </div>
        <div class="clr"></div>
    </div>
    
    <div class="row mt10">
        <span class="rl">&nbsp;</span>
        <div class="fl">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddAdvertisement.OnSave()">提交</a>
        </div>
        <span class="clr"></span>
    </div>
</div>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<div id="dlgUploadPicture" style="padding:10px;"></div>
<div id="dlgMutilSelectPicture" class="easyui-dialog" title="选择图片（多选）" data-options="closed:true,modal:true,href:'/House/t/tpicture.html?dlgId=dlgMutilSelectPicture&funName=PictureAdvertisement&isMutil=true',width:810,height:$(window).height()*0.8,
    buttons: [{
        id:'btnMutilSelectPicture',text:'确定',iconCls:'icon-ok',
        handler:function(){
            DlgPictureSelect.SetMutilPicture('imgContentPicture');
            $('#dlgMutilSelectPicture').dialog('close');
        }
    },{
        id:'btnCancelMutilSelectPicture', text:'取消',iconCls:'icon-cancel',
        handler:function(){
            $('#dlgMutilSelectPicture').dialog('close');
        }
    }],
    toolbar:[{
        id:'dlgMutilSelectPictureToolbarUpload',text:'上传',iconCls:'icon-add',
		handler:function(){
            DlgPictureSelect.DlgUpload();
        }
	}]" style="padding:10px;"></div>

<input type="hidden" id="hId" runat="server" clientidmode="Static" name="hId" />
<script type="text/javascript" src="/House/Scripts/Admin/AddAdvertisement.js"></script>
<script type="text/javascript" src="/House/Scripts/Admin/DlgPictureSelect.js"></script>

<script type="text/javascript">
    var editor_content;
    KindEditor.ready(function (K) {
        editor_content = K.create('#txtContent', {
            uploadJson: '/House/Handlers/Admin/HandlerKindeditor.ashx',
            fileManagerJson: '/House/Handlers/Admin/HandlerKindeditor.ashx',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                });
                K.ctrl(self.edit.doc, 13, function () {
                });
            }
        });
        prettyPrint();

    });

    $(function () {
        try {
            AddAdvertisement.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }

    })

</script>


</asp:Content>
