<%@ Page Title="角色用户分配关系" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddRoleUser.aspx.cs" Inherits="TygaSoft.Web.Admin.Members.AddRoleUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    <div class="fr" style="color:#ff0000;">
        当前角色：<span runat="server" id="lbRole" clientidmode="Static"></span>
    </div>
    <div class="fl">
        用户名：<input type="text" runat="server" clientidmode="Static" id="txtUserName" name="txtUserName" class="txt" maxlength="50" />
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="currFun.Search()">查 询</a>
    </div>
    <span class="clr"></span>
</div>

<table id="bindT" class="easyui-datagrid" title="用户" data-options="fit:true,fitColumns:true,rownumbers:true,pagination:true,singleSelect:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f1',width:200">用户名</th>
            <th data-options="field:'f2',width:200">用户属于角色</th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("UserName")%></td>
                    <td><input type="checkbox" <%#Eval("IsInRole").ToString() == "True" ? "checked='checked'" : ""%> onclick="currFun.CbIsInRole(this)" value='<%#Eval("UserName")%>' /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        var myData = currFun.GetMyData();
        $.map(myData, function (item) {
            sPageIndex = parseInt(item.PageIndex);
            sPageSize = parseInt(item.PageSize);
            sTotalRecord = parseInt(item.TotalRecord);
            sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
        })

        currFun.Init();
    })

    var currFun = {
        Init: function () {
            this.Grid(sPageIndex, sPageSize);
        },
        GetMyData: function () {
            var myData = $("#myDataForPage").html();
            return eval("(" + myData + ")");
        },
        Grid: function (pageIndex, pageSize) {
            var pager = $('#bindT').datagrid('getPager');
            $(pager).pagination({
                total: sTotalRecord,
                pageNumber: sPageIndex,
                pageSize: sPageSize,
                onSelectPage: function (pageNumber, pageSize) {
                    if (sQueryStr.length == 0) {
                        window.location = "?pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                    }
                    else {
                        window.location = "?" + sQueryStr + "&pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                    }
                }
            });
        },
        Search: function () {
            var roleName = $.trim($("#lbRole").text());
            var userName = $.trim($("#txtUserName").val());
            window.location = "?rName=" + roleName + "&uName=" + userName + "";
        },
        CbIsInRole: function (obj) {
            var $_obj = $(obj);
            var roleName = $.trim($("#lbRole").text());
            var userName = $_obj.val();
            var isInRole = $_obj.is(":checked");

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveUserInRole",
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: '{userName:"' + userName + '",roleName:"' + roleName + '",isInRole:"' + isInRole + '"}',
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $("#dlgWaiting").dialog('open');
                },
                complete: function () {
                    $("#dlgWaiting").dialog('close');
                },
                success: function (data) {
                    var msg = data.d;
                    if (msg != "1") {
                        $.messager.alert('系统提示', msg, 'info');
                    }
                    if (!isInRole) {
                        window.location.reload();
                    }
                }
            });
        }
    }
</script>

</asp:Content>
