<%@ Page Title="广告列表" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListAdvertisement.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.ListAdvertisement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">


<div id="toolbar" style="padding:5px;">
    关键字：<input type="text" runat="server" id="txtName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListAdvertisement.Search()">查 詢</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListAdvertisement.Add()">新 增</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListAdvertisement.Edit()">编 辑</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListAdvertisement.Del()">删 除</a>
</div>

<table id="dgT" class="easyui-datagrid" title="广告列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'f1'">标题</th>
            <th data-options="field:'f2'">广告区代码</th>
            <th data-options="field:'f3'">广告区名称</th>
            <th data-options="field:'f4'">广告区值</th>
            <th data-options="field:'f5'">布局位置代码</th>
            <th data-options="field:'f6'">布局位置名称</th>
            <th data-options="field:'f7'">布局位置值</th>
            <th data-options="field:'f8'">切换间隔时间</th>
            <th data-options="field:'f9'">最后更新时间</th>
           
        </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <tr>
                <td><%#Eval("Id")%></td>
                <td><%#Eval("Title")%></td>
                <td><%#Eval("SiteFunCode")%></td>
                <td><%#Eval("SiteFunName")%></td>
                <td><%#Eval("SiteFunValue")%></td>
                <td><%#Eval("LayoutPositionCode")%></td>
                <td><%#Eval("LayoutPositionName")%></td>
                <td><%#Eval("LayoutPositionValue")%></td>
                <td><%#Eval("Timeout")%></td>
                <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:mm")%></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<div id="dlg"></div>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript" src="/House/Scripts/Admin/ListAdvertisement.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        try {
            var myData = ListAdvertisement.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ListAdvertisement.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }

    })
</script>

</asp:Content>
