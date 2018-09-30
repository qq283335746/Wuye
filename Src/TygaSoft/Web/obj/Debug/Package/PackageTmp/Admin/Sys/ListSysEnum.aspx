<%@ Page Title="系统枚举项管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListSysEnum.aspx.cs" Inherits="TygaSoft.Web.Admin.Sys.ListSysEnum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="数据字典" data-options="fit:true">
    <div class="mtb5">
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="SysEnum.Add()">新建</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="SysEnum.Edit()">编辑</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="SysEnum.Del()">删除</a>
    </div>
   <ul id="treeCt" class="easyui-tree" style="padding-left: 5px; margin-top:10px;"></ul>
   <div id="mmTree" class="easyui-menu" style="width:120px;">
       <div onclick="SysEnum.Add()" data-options="iconCls:'icon-add'">添加</div>
       <div onclick="SysEnum.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
       <div onclick="SysEnum.Del()" data-options="iconCls:'icon-remove'">删除</div>
   </div> 
</div>

<div id="dlg" class="easyui-dialog" title="新建/编辑数据字典" data-options="iconCls:'icon-save',closed:true,modal:true,
href:'/House/Templates/AddSysEnum.htm',buttons: [{
	    text:'确定',iconCls:'icon-ok',
	    handler:function(){
		    SysEnum.Save();
	    }
    },{
	    text:'取消',iconCls:'icon-cancel',
	    handler:function(){
		    $('#dlg').dialog('close');
	    }
    }]" style="width:485px;height:390px;padding:10px">

    
</div>

<script type="text/javascript" src="/House/Scripts/Admin/ListSysEnum.js"></script>
<script type="text/javascript">
    $(function () {
        $("#dlg").dialog('refresh', '/House/Templates/AddSysEnum.htm');
        SysEnum.Init();
    })

    
</script>

</asp:Content>
