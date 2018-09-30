<%@ Page Title="业主投诉保修" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListHouseOwnerComplainRepair.aspx.cs" Inherits="TygaSoft.Web.Admin.Repair.ListHouseOwnerComplainRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ComplainRepair.Add()" style="display:none">新 增</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ComplainRepair.Edit()" style="display:none">编 辑</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="ComplainRepair.Deal()">处 理</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ComplainRepair.Del()">删 除</a>
</div>

<table id="dgT" class="easyui-datagrid" title="业主投诉保修列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'Status',hidden:true"></th>
            <th data-options="field:'f1',width:200">住址</th>
            <th data-options="field:'f2',width:100">联系方式</th>
            <th data-options="field:'f3',width:300">描述</th>
            <th data-options="field:'f4',width:80,formatter:ComplainRepair.StatusFormatter">处理状态</th>
            <th data-options="field:'f5',width:120">提交时间</th>
           
        </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <tr>
                <td><%#Eval("Id")%></td>
                <td><%#Eval("Status")%></td>
                <td><%#Eval("Address")%></td>
                <td><%#Eval("Phone")%></td>
                <td><%#Eval("Descri")%></td>
                <td><%#Eval("Status")%></td>
                <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:mm")%></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript" src="/House/Scripts/Admin/ComplainRepair.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {

        try{
            var myData = ComplainRepair.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ComplainRepair.Init();
        } 
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
