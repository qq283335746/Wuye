<%@ Page Title="业主信息列表" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListHouseOwner.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.ListHouseOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    关键字：<input type="text" id="txtKeyword" maxlength="50" class="txt w300" />
    筛选：<select id="cbbParent" class="easyui-combobox">
                <option value="-1">请选择</option>
                <option value="0">物业公司</option>
                <option value="1">小区</option>
                <option value="2">楼</option>
                <option value="3">单元</option>
                <option value="4">房间</option>
                <option value="5">业主</option>
            </select>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ListHouseOwner.Search()">查 詢</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListHouseOwner.Add()">新 增</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListHouseOwner.Edit()">编 辑</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListHouseOwner.Del()">删 除</a>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-man',plain:true" onclick="ListHouseOwner.AddToUser()">分配登录账户</a>
</div>

<table id="dgT" class="easyui-datagrid" title="业主列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
    <thead>
        <tr>
            <th data-options="field:'f0',checkbox:true"></th>
            <th data-options="field:'f1',width:120">业主名称</th>
            <th data-options="field:'f2',width:80">手机号码</th>
            <th data-options="field:'f3',width:80">电话</th>
            <th data-options="field:'f4',width:120">所属物业单位</th>
            <th data-options="field:'f5',width:120">所属小区</th>
            <th data-options="field:'f6',width:80">所属楼</th>
            <th data-options="field:'f7',width:80">所属单元</th>
            <th data-options="field:'f8',width:80">房间号</th>
            <th data-options="field:'f9',width:110">最后更新时间</th>
            <th data-options="field:'f10',hidden:true"></th>
            <th data-options="field:'f11',hidden:true"></th>
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
                <td><%#Eval("CompanyName")%></td>
                <td><%#Eval("CommunityName")%></td>
                <td><%#Eval("BuildingCode")%></td>
                <td><%#Eval("UnitCode")%></td>
                <td><%#Eval("HouseCode")%></td>
                <td><%#((DateTime)Eval("LastUpdatedDate")).ToString("yyyy-MM-dd HH:ss")%></td>
                <td><%#Eval("UserName")%></td>
                <td><%#Eval("Password")%></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </tbody>
</table>

<asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

<div id="dlg" class="easyui-dialog" title="分配登录账户" data-options="iconCls:'icon-man',closed:true,modal:true,border:false,
buttons: [{
	text:'确定',iconCls:'icon-ok',
	handler:function(){
		ListHouseOwner.OnDlgSave();
	}
},{
	text:'取消',iconCls:'icon-cancel',
	handler:function(){
		$('#dlg').dialog('close');
	}
}]" style="width:420px;height:200px;padding:10px;">
    <div class="row">
        <span class="rl" style="width:70px;"><b class="cr">*</b>账号：</span>
        <div class="fl">
            <input type="text" id="txtUserName" class="txt" />
        </div>
    </div>
    <div class="row mt10">
        <span class="rl" style="width:70px;"><b class="cr">*</b>密码：</span>
        <div class="fl">
            <input type="text" id="txtPsw" class="txt" />
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-help',plain:true" onclick="ListHouseOwner.GetPswByRandom()">随机生成</a>
        </div>
    </div>
    <input type="hidden" id="hHouseOwnerId" />
</div>

<script type="text/javascript" src="/House/Scripts/Admin/ListHouseOwner.js"></script>

<script type="text/javascript">
    var sPageIndex = 0;
    var sPageSize = 0;
    var sTotalRecord = 0;
    var sQueryStr = "";

    $(function () {
        try {
            var myData = ListHouseOwner.GetMyData("myDataForPage");
            $.map(myData, function (item) {
                sPageIndex = parseInt(item.PageIndex);
                sPageSize = parseInt(item.PageSize);
                sTotalRecord = parseInt(item.TotalRecord);
                sQueryStr = item.QueryStr.replace(/&amp;/g, '&');
            })

            ListHouseOwner.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })
</script>

</asp:Content>
