<%@ Page Title="类别字典" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListContentType.aspx.cs" Inherits="TygaSoft.Web.Admin.AboutSite.ListContentType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="类别字典" data-options="fit:true">
    <div class="mtb5">
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListContentType.Add()">新建</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListContentType.Edit()">编辑</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListContentType.Del()">删除</a>
    </div>
   <ul id="treeCt" class="easyui-tree" style="padding-left: 5px; margin-top:10px;"></ul>
   <div id="mmTree" class="easyui-menu" style="width:120px;">
       <div onclick="ListContentType.Add()" data-options="iconCls:'icon-add'">添加</div>
       <div onclick="ListContentType.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
       <div onclick="ListContentType.Del()" data-options="iconCls:'icon-remove'">删除</div>
   </div> 
</div>

<div id="dlg" class="easyui-dialog" title="新建/编辑数据字典" data-options="iconCls:'icon-save',closed:true,modal:true,
href:'/House/Templates/AddContentType.htm',buttons: [{
	    text:'确定',iconCls:'icon-ok',
	    handler:function(){
		    ListContentType.Save();
	    }
    },{
	    text:'取消',iconCls:'icon-cancel',
	    handler:function(){
		    $('#dlg').dialog('close');
	    }
    }]" style="width:490px;height:390px;padding:10px">

    
</div>

<script type="text/javascript" src="/House/Scripts/Admin/ListContentType.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            $("#dlg").dialog('refresh', '/House/Templates/AddContentType.htm');
            ListContentType.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })

    
</script>

</asp:Content>
