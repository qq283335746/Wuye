<%@ Page Title="新建小区信息" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddResidenceCommunity.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.AddResidenceCommunity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息" data-options="fit:true" style="padding:10px;"> 

    <div class="row">
        <span class="fl rl"><b class="cr">*</b>小区名称：</span>
        <div class="fl">
            <input type="text" id="txtName" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>所属物业单位：</span>
        <div class="fl">
            <a href="javascript:void(0)" id="lbtnSelect" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="AddResidenceCommunity.OnDlgCompany()">选 择</a>
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>省市区：</span>
        <div class="fl">
            <ul id="cbtProvinceCity" class="easyui-combotree" data-options="required:true,missingMessage:'请选择省市区'"></ul>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>详细地址：</span>
        <div class="fl">
            <input type="text" id="txtAddress" runat="server" clientidmode="Static" class="easyui-validatebox" data-options="required:true" style="width:275px;" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mt10">
        <span class="fl rl">简介：</span>
        <div class="fl">
            <textarea id="txtaDescri" runat="server" clientidmode="Static" rows="3" cols="90" style="width:100%; height:80px;"></textarea>
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddResidenceCommunity.OnSave()">提 交</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

<input type="hidden" id="hId" runat="server" clientidmode="Static" />
<input type="hidden" id="hPropertyCompanyId" />
<input type="hidden" id="hTreeParentAppend" />
<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<div id="dlgCompany" class="easyui-dialog" title="物业公司列表" data-options="iconCls:'icon-search',closed:true,modal:true,
buttons: [{
	text:'确定',iconCls:'icon-ok',
	handler:function(){
		AddResidenceCommunity.OnSelectCompany();
	}
},{
	text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlgCompany').dialog('close');
	}
}]" style="width:780px;height:400px;padding:10px">

<div id="toolbar" style="padding:5px;">
    公司名称：<input type="text" id="txtCompanyName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="AddResidenceCommunity.OnSearchCompany()">查 詢</a>
</div>

<table id="dgCompany" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,singleSelect:true,fit:true,fitColumns:true,toolbar:'#toolbar',
url:'/House/Handlers/Admin/HandlerPropertyCompany.ashx?reqName=GetJsonForDatagrid',method:'GET',onSelectPage:AddResidenceCommunity.OnSelectPage">
    <thead>
        <tr>
            <th data-options="field:'Id',checkbox:true"></th>
            <th data-options="field:'CompanyName',width:120">公司名称</th>
            <th data-options="field:'ShortName',width:100">公司简称</th>
            <th data-options="field:'Province',width:80">省</th>
            <th data-options="field:'City',width:80">市</th>
            <th data-options="field:'District',width:80">区</th>
           
        </tr>
    </thead>
</table>
    
</div>

<script type="text/javascript" src="/House/Scripts/Admin/AddResidenceCommunity.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddResidenceCommunity.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
