<%@ Page Title="新建/编辑投诉保修信息" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddComplainRepair.aspx.cs" Inherits="TygaSoft.Web.Admin.Repair.AddComplainRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="填写信息" data-options="fit:true" style="padding:10px;"> 

    <div class="row">
        <span class="fl rl"><b class="cr">*</b>投诉保修类别：</span>
        <div class="fl">
            <select id="cbbRepairCategory" class="easyui-combobox" data-options="valueField:'id',textField:'text',url:'/House/Handlers/Admin/HandlerSysEnum.ashx?reqName=GetJsonForCbbRepair',method:'Get'" style="width:200px;"></select>
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mt10">
        <span class="fl rl"><b class="cr">*</b>联系方式：</span>
        <div class="fl">
            <input type="text" id="txtPhone" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
   <div class="row mtb10">
        <span class="fl rl"><b class="cr">*</b>住址：</span>
        <div class="fl">
            <input type="text" id="txtAddress" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl"><b class="cr">*</b>描述：</span>
        <div class="fl">
            <input type="text" id="txtDescri" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
    <div class="row mtb10">
        <span class="fl rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="AddComplainRepair.OnSave()">提 交</a>
        </div>
        <span class="clr"></span>
    </div>

</div>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
<script type="text/javascript" src="/House/Scripts/Admin/AddComplainRepair.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            AddComplainRepair.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
