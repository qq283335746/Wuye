<%@ Page Title="省市区管理" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ListProvinceCity.aspx.cs" Inherits="TygaSoft.Web.Admin.BasicData.ListProvinceCity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="省市区" data-options="fit:true">
    <div class="mtb5">
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ProvinceCity.Add()">新建</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ProvinceCity.Edit()">编辑</a>
       <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ProvinceCity.Del()">删除</a>
    </div>
   <ul id="treeCt" class="easyui-tree" style="padding-left: 5px;"></ul>
   <div id="mmTree" class="easyui-menu" style="width:120px;">
       <div onclick="ProvinceCity.Add()" data-options="iconCls:'icon-add'">添加</div>
       <div onclick="ProvinceCity.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
       <div onclick="ProvinceCity.Del()" data-options="iconCls:'icon-remove'">删除</div>
   </div> 
</div>

<div id="dlg" class="easyui-dialog" title="新建/编辑数据字典" data-options="iconCls:'icon-save',closed:true,modal:true,
href:'/House/Templates/AddProvinceCity.htm',buttons: [{
	    text:'确定',iconCls:'icon-ok',
	    handler:function(){
		    ProvinceCity.Save();
	    }
    },{
	    text:'取消',iconCls:'icon-cancel',
	    handler:function(){
		    $('#dlg').dialog('close');
	    }
    }]" style="width:485px;height:390px;padding:10px">

    
</div>

<script type="text/javascript" src="/House/Scripts/Admin/ProvinceCity.js"></script>
<script type="text/javascript">
    $(function () {
        try {
            $("#dlg").dialog('refresh', '/House/Templates/AddProvinceCity.htm');
            ProvinceCity.Init();
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
        
    })

    
</script>

</asp:Content>
