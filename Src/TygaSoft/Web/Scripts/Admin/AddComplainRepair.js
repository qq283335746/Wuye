
var AddComplainRepair = {
    Init: function () {
        this.SetForm();
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    SetForm: function () {
        $.map(AddComplainRepair.GetMyData("myDataForModelInfo"), function (item) {
            
        })
    },
    OnSave: function () {
        try {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            var Id = "";
            $.map(AddComplainRepair.GetMyData("myDataForModelInfo"), function (item) {
                Id = item.Id;
            })

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveComplainRepair",
                type: "post",
                data: '{model:{Id:"' + Id + '",SysEnumId:"' + $('#cbbRepairCategory').combobox('getValue') + '",Phone:"' + $.trim($("#txtPhone").val()) + '",Address:"' + $.trim($("#txtAddress").val()) + '",Descri:"' + $.trim($("#txtDescri").val()) + '"}}',
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
                    }
                    else {
                        $.messager.alert('系统提示', msg, 'info');
                    }
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    }


} 