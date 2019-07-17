<%@ Page Title="新建房间信息" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddHouse.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.AddHouse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息" data-options="fit:true" style="padding:10px;"> 

   <div class="row">
        <span class="fl rl" style="width:200px;"><b class="cr">*</b>房号：</span>
        <div class="fl">
            <input type="text" id="txtName" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
   </div>
   <div class="row mt10">
        <span class="fl rl" style="width:200px;"><b class="cr">*</b>父级：</span>
        <div class="fl">
            <a href="javascript:void(0)" id="lbtnSelect" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="AddHouse.OnDlgOpenClick()">选 择</a>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row">
        <span class="fl rl" style="width:200px;">房屋面积（单位：平方）：</span>
        <div class="fl">
            <input type="text" id="txtHouseAcreage" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="validType:'float'" />
        </div>
        <span class="clr"></span>
   </div>
    <div class="row mt10">
        <span class="fl rl" style="width:200px;">备注：</span>
        <div class="fl">
            <textarea id="txtaRemark" runat="server" clientidmode="Static" rows="3" cols="90" style="width:100%; height:80px;"></textarea>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl" style="width:200px;">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddHouse.OnSave()">提 交</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

<input type="hidden" id="hId" runat="server" clientidmode="Static" />
<input type="hidden" id="hPropertyCompanyId" />
<input type="hidden" id="hResidenceCommunityId" />
<input type="hidden" id="hResidentialBuildingId" />
<input type="hidden" id="hResidentialUnitId" />
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
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouse.OnSearchBuilding()">查 詢</a>
</div>
<div id="dgUnitToolbar" style="padding:5px;">
    单元号：<input type="text" id="txtUnitCode" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouse.OnSearchUnit()">查 詢</a>
</div>

<div id="dlg" class="easyui-dialog" title="选择所属父级" data-options="iconCls:'icon-search',closed:true,modal:true,border:false,
buttons: [{
	text:'确定',iconCls:'icon-ok',
	handler:function(){
		AddHouse.OnDlgSelect();
	}
},{
	text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlg').dialog('close');
	}
}],onOpen:AddHouse.OnDlgOpen()" style="width:780px;height:400px;">
    <div id="tabParent" class="easyui-tabs" data-options="border:false,fit:true"></div>
</div>

<script type="text/javascript" src="/House/Scripts/Admin/AddHouse.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddHouse.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    })
</script>

</asp:Content>
