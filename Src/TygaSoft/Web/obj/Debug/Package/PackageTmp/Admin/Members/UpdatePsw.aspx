<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UpdatePsw.aspx.cs" Inherits="TygaSoft.Web.Admin.Members.UpdatePsw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="修改密码" data-options="fit:true" style="padding:10px;"> 

    <div class="row">
        <span class="rl">当前密码：</span>
        <div class="fl">
            <input type="password" runat="server" id="txtOldPsw" class="easyui-validatebox  txt" data-options="required:true,validType:'psw'" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">新密码：</span>
        <div class="fl">
            <input type="password" runat="server" id="txtNewPsw" clientidmode="Static" class="easyui-validatebox  txt" data-options="required:true,validType:'psw'" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">确认密码：</span>
        <div class="fl">
            <input type="password" runat="server" id="txtCfmPsw" class="easyui-validatebox  txt" data-options="required:true" validType="cfmPsw['#txtNewPsw']" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">&nbsp;</span>
        <div class="fl">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="currFun.Save()">保 存</a>
        </div>
    </div>

</div>

<script type="text/javascript">
    $(function () {
        $(document).bind("keydown", function (e) {
            var key = e.which;
            if (key == 13) {
                currFun.Save();
            }
        })
    })

    var currFun = {

        Save: function () {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            $('#form1').submit();
        }
    }
</script>

</asp:Content>
