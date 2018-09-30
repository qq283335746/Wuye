<%@ Page Title="新建业主信息" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddHouseOwner.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.AddHouseOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="/House/Scripts/JeasyuiExtend.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息" data-options="fit:true" style="padding:10px;"> 

   <div class="row">
        <span class="fl rl" style="width:140px;"><b class="cr">*</b>业主名称：</span>
        <div class="fl">
            <input type="text" id="txtName" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl" style="width:140px;"><b class="cr">*</b>手机号码：</span>
        <div class="fl">
            <input type="text" id="txtMobilePhone" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="required:true,validType:'phone'" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl" style="width:140px;">电话：</span>
        <div class="fl">
            <input type="text" id="txtTelPhone" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="validType:'telPhone'" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl" style="width:140px;"><b class="cr">*</b>父级：</span>
        <div class="fl">
            <a href="javascript:void(0)" id="lbtnSelect" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="AddHouseOwner.OnDlgOpenClick()">选 择</a>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl" style="width:140px;">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddHouseOwner.OnSave()">提 交</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

<input type="hidden" id="hId" runat="server" clientidmode="Static" />
<input type="hidden" id="hPropertyCompanyId" />
<input type="hidden" id="hResidenceCommunityId" />
<input type="hidden" id="hResidentialBuildingId" />
<input type="hidden" id="hResidentialUnitId" />
<input type="hidden" id="hHouseId" />
<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<div id="dgCompanyToolbar" style="padding:5px;">
    公司名称：<input type="text" id="txtCompanyName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouse.OnSearchCompany()">查 詢</a>
</div>
<div id="dgCommunityToolbar" style="padding:5px;">
    小区名称：<input type="text" id="txtCommunityName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouse.OnSearchCommunity()">查 詢</a>
</div>
<div id="dgBuildingToolbar" style="padding:5px;">
    楼号：<input type="text" id="txtBuildingCode" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouseOwner.OnSearchBuilding()">查 詢</a>
</div>
<div id="dgUnitToolbar" style="padding:5px;">
    单元号：<input type="text" id="txtUnitCode" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouseOwner.OnSearchUnit()">查 詢</a>
</div>
<div id="dgHouseToolbar" style="padding:5px;">
    房间号：<input type="text" id="txtHouseCode" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouseOwner.OnSearchHouse()">查 詢</a>
</div>

<div id="dlg" class="easyui-dialog" title="选择所属父级" data-options="iconCls:'icon-search',closed:true,modal:true,border:false,
buttons: [{
	text:'确定',iconCls:'icon-ok',
	handler:function(){
		AddHouseOwner.OnDlgSelect();
	}
},{
	text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlg').dialog('close');
	}
}],onOpen:AddHouseOwner.OnDlgOpen()" style="width:780px;height:400px;">
    <div id="tabParent" class="easyui-tabs" data-options="border:false,fit:true"></div>
</div>

<script type="text/javascript" src="/House/Scripts/Admin/AddHouseOwner.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddHouseOwner.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
