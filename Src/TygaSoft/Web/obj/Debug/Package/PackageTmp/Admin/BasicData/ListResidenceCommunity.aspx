<%@ Page Title="小区列表" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListResidenceCommunity.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.ListResidenceCommunity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    小区名称：<input type="text" runat="server" id="txtName" maxlength="50" class="txt" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListResidenceCommunity.Search()">查 詢</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListResidenceCommunity.Add()">新 增</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListResidenceCommunity.Edit()">编 辑</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListResidenceCommunity.Del()">删 除</a>
</div>

<table id="dgT" class="easyui-datagrid" title="小区列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'f1',width:120">小区名称</th>
            <th data-options="field:'f2',width:120">所属物业单位</th>
            <th data-options="field:'f3',width:80">省</th>
            <th data-options="field:'f4',width:80">市</th>
            <th data-options="field:'f5',width:80">区</th>
            <th data-options="field:'f6',width:100">详细地址</th>
            <th data-options="field:'f7',width:200">简介</th>
            <th data-options="field:'f8',width:110">最后更新时间</th>
           
        </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <tr>
                <td><%#Eval("Id")%></td>
                <td><%#Eval("CommunityName")%></td>
                <td><%#Eval("CompanyName")%></td>
                <td><%#Eval("Province")%></td>
                <td><%#Eval("City")%></td>
                <td><%#Eval("District")%></td>
                <td><%#Eval("Address")%></td>
                <td><%#Eval("AboutDescri").ToString().Length > 16 ? Eval("AboutDescri").ToString().Substring(0,16) + "......":Eval("AboutDescri")%></td>
                <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:ss")%></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript" src="/House/Scripts/Admin/ListResidenceCommunity.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        try {
            var myData = ListResidenceCommunity.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ListResidenceCommunity.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
