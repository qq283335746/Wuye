<%@ Page Title="分配业主账号" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListUserHouseOwner.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.ListUserHouseOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="mtb10">
    当前要分配账号：<span id="lbUser" runat="server" clientidmode="Static" class="cr"></span>
</div>

<div id="toolbar" style="padding:5px;">
    业主名称：<input type="text" runat="server" id="txtName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListUserHouseOwner.Search()">查 詢</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListUserHouseOwner.AddUserHouseOwner()">绑定业主</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListUserHouseOwner.Del()">取消绑定</a>
</div>

<table id="dgT" class="easyui-datagrid" title="已分配业主列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'f1',width:120">业主名称</th>
            <th data-options="field:'f2',width:80">手机号码</th>
            <th data-options="field:'f3',width:80">电话</th>
        </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <tr>
                <td><%#Eval("Id")%></td>
                <td><%#Eval("HouseOwnerName")%></td>
                <td><%#Eval("MobilePhone")%></td>
                <td><%#Eval("TelPhone")%></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<div id="dlgUserHouseOwner">
    <div id="dgHouseOwnerToolbar" style="padding:5px;">
        业主名称：<input type="text" id="txtHouseOwnerName" maxlength="50" class="txt" />
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListUserHouseOwner.DgHouseOwnerSearch()">查 詢</a>
    </div>
    <table class="easyui-datagrid" id="dgHouseOwner"
        data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,url:'/h/t.html?reqName=GetJsonForDatagrid',method:'get',toolbar:'#dgHouseOwnerToolbar',onCheck:ListUserHouseOwner.DgHouseOwnerOnCheck,onUncheck:ListUserHouseOwner.DgHouseOwnerOnUncheck">
        <thead>
		    <tr>
                <th data-options="field:'Id',checkbox:true">业主名称</th>
			    <th data-options="field:'HouseOwnerName',width:100">业主名称</th>
			    <th data-options="field:'MobilePhone',width:100">手机号码</th>
			    <th data-options="field:'CompanyName',width:100">物业公司</th>
                <th data-options="field:'CommunityName',width:100">小区</th>
                <th data-options="field:'BuildingCode',width:100">楼</th>
                <th data-options="field:'UnitCode',width:100">小区</th>
                <th data-options="field:'HouseCode',width:100">房间</th>
		    </tr>
        </thead>
    </table>
    <input type="hidden" id="hSuccessCount" value="0" />
</div>

<script type="text/javascript" src="/House/Scripts/Admin/ListUserHouseOwner.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        try {
            var myData = ListUserHouseOwner.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ListUserHouseOwner.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
