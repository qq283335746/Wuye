<%@ Page Title="下发通知" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddHouseOwnerNotice.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.AddHouseOwnerNotice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="下发通知" data-options="fit:true" style="padding:10px;"> 

    <div class="row">
        <span class="fl rl"><b class="cr">*</b>下发通知至：</span>
        <div class="fl">
            <select id="cbbType" class="easyui-combobox" style="width:200px;">
                <option value="-1">请选择</option>
                <option value="0">下发至物业公司</option>
                <option value="1">下发至小区</option>
                <option value="2">下发至楼</option>
                <option value="3">下发至单元</option>
                <option value="4">下发至房间</option>
                <option value="5">下发至业主</option>
            </select>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>业主：</span>
        <div class="fl">
            <a href="javascript:void(0)" id="abtnHouseOwner" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="AddHouseOwnerNotice.OnDlgHouseOwner()">选 择</a>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>通知列表：</span>
        <div class="fl">
            <a href="javascript:void(0)" id="abtnNotice" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="AddHouseOwnerNotice.OnDlgNotice()">选 择</a>
        </div>
        <span class="clr"></span>
    </div>
    
    <div class="row mtb10">
        <span class="fl rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddHouseOwnerNotice.OnSave()">发 送</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

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
<div id="dgHouseOwnerToolbar" style="padding:5px;">
    业主：<input type="text" id="txtHouseOwnerName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouseOwner.OnSearchHouseOwner()">查 詢</a>
</div>
<div id="dgNoticeToolbar" style="padding:5px;">
    标题：<input type="text" id="txtNoticeTitle" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddHouseOwner.OnSearchNotice()">查 詢</a>
</div>

<div id="dlgHouseOwner" class="easyui-dialog" title="选择" data-options="iconCls:'icon-search',closed:true,modal:true,border:false,
buttons: [{
	id:'btnSelectHouseOwner',text:'确定',iconCls:'icon-ok',
	handler:function(){
		AddHouseOwnerNotice.OnDlgHouseOwnerSelect();
	}
},{
	id:'btnCancelSelectHouseOwner',text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlgHouseOwner').dialog('close');
	}
}],onOpen:AddHouseOwnerNotice.OnDlgHouseOwnerOpen()" style="width:780px;height:400px;">
    <div id="tabHouseOwner" class="easyui-tabs" data-options="border:false,fit:true"></div>
</div>

<div id="dlgNotice" class="easyui-dialog" title="选择通知" data-options="iconCls:'icon-search',closed:true,modal:true,border:false,
buttons: [{
	id:'btnSelectNotice', text:'确定',iconCls:'icon-ok',
	handler:function(){
		AddHouseOwnerNotice.OnDlgNoticeSelect();
	}
},{
	id:'btnCancelSelectNotice',text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlgNotice').dialog('close');
	}
}]" style="width:780px;height:400px;">
    
    <table  id="dgNotice" class="easyui-datagrid" data-options="border:false,fit:true,rownumbers: true, pagination: true,url:'/House/Handlers/Admin/HandlerAnnouncement.ashx',method:'get',queryParams:{reqName:'GetJsonForNoticeDatagrid',title: $.trim($('#txtNoticeTitle').val())},toolbar: '#dgNoticeToolbar'">
        <thead>
		    <tr>
                <th data-options="field:'Id',checkbox:true"></th>
			    <th data-options="field:'Title'">标题</th>
		    </tr>
        </thead>
    </table>
</div>

<script type="text/javascript" src="/House/Scripts/Admin/AddHouseOwnerNotice.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddHouseOwnerNotice.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
