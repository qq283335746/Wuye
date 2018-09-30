<%@ Page Title="新建/编辑通知" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddNotice.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.AddNotice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <link href="/House/Scripts/plugins/kindeditor/themes/default/default.css" rel="stylesheet" type="text/css" />
  <link href="/House/Scripts/plugins/kindeditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" /> 
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
        <span class="rl"><b class="cr">*</b>功能模块：</span>
        <div class="fl">
            <input id="txtParent" runat="server" clientidmode="Static" class="easyui-combotree" style="width:200px;" />
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="rl">描述说明：</span>
        <div class="fl" style="width:791px;">
            <textarea id="txtaDescr" runat="server" clientidmode="Static" rows="3" cols="80" class="txta" style="width:100%;"></textarea>
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="rl"><b class="cr">*</b>内容：</span>
        <div class="fl">
            <textarea id="txtContent" runat="server" clientidmode="Static" cols="100" rows="8" style="width:800px;height:800px;"></textarea>
        </div>
        <div class="clr"></div>
    </div>
    <div class="row mt10">
        <span class="rl">&nbsp;</span>
        <div class="fl">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddNotice.OnSave()">提交</a>
        </div>
        <span class="clr"></span>
    </div>
</div>

<input type="hidden" id="hId" runat="server" clientidmode="Static" name="hId" />
<script type="text/javascript" src="/House/Scripts/Admin/AddNotice.js"></script>

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
            AddNotice.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })

</script>

</asp:Content>
