<%@ Page Title="广告相册管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListPictureAdvertisement.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.ListPictureAdvertisement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding:5px;">
        开始日期：<input id="txtStartDate" class="easyui-datebox" />
        结束日期：<input id="txtEndDate" class="easyui-datebox" />
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListPictureAdvertisement.Search()">查 詢</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListPictureAdvertisement.Add()">上 传</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListPictureAdvertisement.Del()">删 除</a>
    </div>

    <table id="dgT" class="easyui-datagrid" title="广告相册列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'f0',checkbox:true"></th>
                <th data-options="field:'f1'">图片</th>
                <th data-options="field:'f2'">文件名</th>
                <th data-options="field:'f3'">文件大小</th>
                <th data-options="field:'f4'">文件虚拟目录</th>
                <th data-options="field:'f5'">缩略图文件夹</th>
                <th data-options="field:'f6'">日期</th>
                <th data-options="field:'f7'">原图</th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Id")%></td>
                    <td><img src='<%#string.Format("{0}{1}/PC/{1}_2{2}",Eval("FileDirectory"),Eval("RandomFolder"),Eval("FileExtension")) %>' alt="" width="50px" height="50px" /> </td>
                    <td><%#Eval("FileName")%></td>
                    <td><%#Eval("FileSize")%></td>
                    <td><%#Eval("FileDirectory")%></td>
                    <td><%#Eval("RandomFolder")%></td>
                    <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:mm")%></td>
                    <td><a href="javascript:void(0)" code='<%#string.Format("{0}{1}",Eval("FileDirectory"),Eval("FileName")) %>' onclick='ListPictureAdvertisement.ViewPic(this)'>查看原图</a> </td>
                
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </tbody>
    </table>

    <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
    <div id="dlgViewPic" style="padding:10px;"></div>
    <div id="dlgUploadPicture" style="padding: 10px;"></div>

    <script type="text/javascript" src="/House/Scripts/Admin/DlgPictureSelect.js"></script>
    <script type="text/javascript" src="/House/Scripts/Admin/ListPictureAdvertisement.js"></script>

    <script type="text/javascript">
        var sPageIndex = 0;
        var sPageSize = 0;
        var sTotalRecord = 0;
        var sQueryStr = "";

        $(function () {
            try {
                var myData = ListPictureAdvertisement.GetMyData("myDataForPage");
                $.map(myData, function (item) {
                    sPageIndex = parseInt(item.PageIndex);
                    sPageSize = parseInt(item.PageSize);
                    sTotalRecord = parseInt(item.TotalRecord);
                    sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
                })

                ListPictureAdvertisement.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }

        })
    </script>

</asp:Content>
