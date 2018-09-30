<%@ Page Title="新建物业公司信息" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddPropertyCompany.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.AddPropertyCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息" data-options="fit:true" style="padding:10px;"> 

    <div class="row">
        <span class="fl rl"><b class="cr">*</b>公司名称：</span>
        <div class="fl">
            <input type="text" id="txtCompanyName" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>公司简称：</span>
        <div class="fl">
            <input type="text" id="txtShortName" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mtb10">
        <span class="fl rl"><b class="cr">*</b>省市区：</span>
        <div class="fl">
            <ul id="cbtProvinceCity" class="easyui-combotree"></ul>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddPropertyCompany.OnSave()">提 交</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

<input type="hidden" id="hTreeParentAppend" />
<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
<script type="text/javascript" src="/House/Scripts/Admin/AddPropertyCompany.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddPropertyCompany.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
