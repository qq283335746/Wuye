<%@ Page Title="内容相册列表" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListContentPicture.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.ListContentPicture" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    开始日期：<input id="txtStartDate" class="easyui-datebox" />
    结束日期：<input id="txtEndDate" class="easyui-datebox" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListContentPicture.Search()">查 詢</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListContentPicture.Add()">上 传</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListContentPicture.Del()">删 除</a>
</div>
<table id="dgT" class="easyui-datagrid" title="内容相册列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'f1'">图片</th>
            <th data-options="field:'f2'">上传日期</th>
            <th data-options="field:'f3'">原图</th>
        </tr>
    </thead>
    <tbody>
    <asp:Repeater ID="rpData" runat="server">
        <ItemTemplate>
            <tr>
                <td><%#Eval("Id")%></td>
                <td><img src='../..<%#Eval("OriginalPicture")%>' alt="" width="50px" height="50px"  /> </td>
                <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:mm")%></td>
                <td><a href="javascript:void(0)" code='<%#Eval("OriginalPicture")%>' onclick='ListContentPicture.ViewPic(this)'>查看原图</a> </td>
                
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
<div id="dlgViewPic" style="padding:10px;"></div>
<div id="dlgUpload" style="padding:10px;"></div>

<script type="text/javascript" src="/House/Scripts/Admin/ListContentPicture.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        try {
            var myData = ListContentPicture.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ListContentPicture.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }

    })
</script>

</asp:Content>
