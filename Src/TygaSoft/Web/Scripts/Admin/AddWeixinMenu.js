
var AddWeixinMenu = {
    OnSave: function () {
        try {
            var sMenuJson = $.trim($("#txtaMenuJson").val());
            if (sMenuJson == "") {
                $.messager.alert('错误提示', '输入不能为空字符串', 'error');
                return false;
            }
            var objMenuJson = $.parseJSON(sMenuJson);
            if (objMenuJson == null) {
                $.messager.alert('错误提示', '输入字符串无法格式为json字符串，请检查', 'error');
                return false;
            }
            sMenuJson = "<![CDATA[" + sMenuJson + "]]>";
            var aa = "123";
            $.messager.confirm('温馨提醒', '确定要提交吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/SaveWeixinMenu",
                        type: "post",
                        data: '{itemsAppend:"' + sMenuJson + '"}',
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            $("#dlgWaiting").dialog('open');
                        },
                        complete: function () {
                            $("#dlgWaiting").dialog('close');
                        },
                        success: function (data) {
                            var msg = data.d;
                            if (msg == "1") {
                                jeasyuiFun.show("温馨提示", "保存成功！");
                                $('#dlg').dialog('close');
                            }
                            else {
                                $.messager.alert('系统提示', msg, 'info');
                            }
                        }
                    });
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    }
}