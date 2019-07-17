<%@ Page Title="成员管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListUsers.aspx.cs" Inherits="TygaSoft.Web.Admin.Members.ListUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-layout" data-options="fit:true">
    <div data-options="region:'east',title:'',split:true" style="width:230px;">
        <div id="toolbarRole" style="padding:5px;">
            将“<span id="lbUserName"></span>”添加到角色:
        </div>
        <table id="dgRole" class="easyui-datagrid" title="角色" data-options="fit:true,rownumbers:true,singleSelect:true,border:false,toolbar:'#toolbarRole',onLoadSuccess:ListUser.OnRoleLoadSuccess">
            <thead>
            <tr>
                <th data-options="hidden:true,field:'IsInRole'"></th>
                <th data-options="field:'RoleName',formatter:ListUser.RoleFormatter"></th>
            </tr>
        </thead>
        </table>
    </div>
    <div data-options="region:'center',title:''">
        <div id="toolbar" style="padding:5px;">
            用户名：<input type="text" runat="server" id="txtUserName" maxlength="50" class="txt" />
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListUser.Search()">查 询</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListUser.Add()">新建</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListUser.Del()">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListUser.ResetPassword()">重置密码</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListUser.AddUserHouseOwner()">分配业主</a>
        </div>

        <table id="bindT" class="easyui-datagrid" title="用户列表" data-options="fit:true,fitColumns:true,rownumbers:true,pagination:true,singleSelect:true,border:false,toolbar:'#toolbar'">
        <thead>
            <tr>
            <th data-options="field:'f0',hidden:true"></th>
            <th data-options="field:'f1',width:100">用户名</th>
            <th data-options="field:'f2',hidden:true">电子邮箱</th>
            <th data-options="field:'f3',width:100">创建日期</th>
            <th data-options="field:'f4',width:100">最后一次登录时间</th>
            <th data-options="field:'f5',width:100">是否启用</th>
            <th data-options="field:'f6',width:100">是否锁定</th>
            <th data-options="field:'f7',width:100">角色</th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("UserName")%></td>
                    <td><%#Eval("UserName")%></td><td><%#Eval("Email")%></td><td><%#((DateTime)Eval("CreationDate")).ToString("yyyy-MM-dd HH:mm")%></td>
                    <td><%#((DateTime)Eval("LastLoginDate")).ToString("yyyy-MM-dd HH:mm")%></td>
                    <td><a href="javascript:void(0)" onclick="ListUser.OnIsApproved(this)" code='<%#Eval("UserName") %>'><%#Eval("IsApproved").ToString() == "False" ? "禁用" : "启用"%></a></td>
                    <td><a href="javascript:void(0)" onclick="ListUser.OnIsLockedOut(this)" code='<%#Eval("UserName") %>'><%#Eval("IsLockedOut").ToString() == "1" ? "已锁定":"正常" %></a></td>
                    <td><a href="javascript:void(0)" onclick="ListUser.EditRole('<%#Eval("UserName") %>')">编辑角色</a></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </tbody>
        </table>
    </div>
</div>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<script type="text/javascript" src="/House/Scripts/Admin/ListUser.js"></script>

<script type="text/javascript">

    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        var myData = ListUser.GetMyData($("#myDataForPage"));
        $.map(myData, function (item) {
            sPageIndex = parseInt(item.PageIndex);
            sPageSize = parseInt(item.PageSize);
            sTotalRecord = parseInt(item.TotalRecord);
            sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
        })

        ListUser.Init();
    }) 
</script>

</asp:Content>
