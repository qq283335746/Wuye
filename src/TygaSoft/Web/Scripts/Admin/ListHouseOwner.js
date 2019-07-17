
var ListHouseOwner = {
    Init: function () {
        this.Grid(sPageIndex, sPageSize);
        this.InitSearchCondition();
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    Grid: function (pageIndex, pageSize) {
        var pager = $('#dgT').datagrid('getPager');
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
        window.location = "?parentType=" + $("#cbbParent").combobox('getValue').replace("-1", "") + "&keyword=" + $.trim($("#txtKeyword").val()) + "";
    },
    InitSearchCondition: function () {
        var myDataForSearch = ListHouseOwner.GetMyData("myDataForSearch");
        $.map(myDataForSearch, function (item) {
            if (item.parentType != "") {
                $("#cbbParent").combobox('setValue', item.parentType);
            }
            $("#txtKeyword").val(item.keyword);
        })
    },
    Add: function () {
        window.location = "ytg.html";
    },
    Edit: function () {
        var cbl = $('#dgT').datagrid("getSelections");
        if (cbl && cbl.length == 1) {
            window.location = "ytg.html?Id=" + cbl[0].f0 + "";
        }
        else {
            $.messager.alert('错误提醒', '请选择一行且仅一行进行编辑', 'error');
        }
    },
    Del: function () {
        try {
            var rows = $('#dgT').datagrid("getSelections");
            if (!rows || rows.length == 0) {
                $.messager.alert('错误提醒', '请至少选择一行再进行操作', 'error');
                return false;
            }
            var itemsAppend = "";
            for (var i = 0; i < rows.length; i++) {
                if (i > 0) itemsAppend += ",";
                itemsAppend += rows[i].f0;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/DelHouseOwner",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: '{itemsAppend:"' + itemsAppend + '"}',
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
                                return false;
                            }
                            window.location.reload();
                        }
                    });
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    AddToUser: function () {
        var rows = $('#dgT').datagrid("getSelections");
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提醒', '请选择一行且仅一行进行此操作', 'error');
            return false;
        }
        $("#txtUserName").val(rows[0].f10);
        $("#txtPsw").val(rows[0].f11);
        $("#hHouseOwnerId").val(rows[0].f0);
        $("#dlg").dialog('open');
    },
    OnDlgSave: function () {
        var houseOwnerId = $("#hHouseOwnerId").val();
        var sUserName = $.trim($("#txtUserName").val());
        var sPassword = $.trim($("#txtPsw").val());
        if (sUserName == "" || sPassword == "") {
            $.messager.alert('错误提示', "有“*”标识的为必填项，请检查！", 'error');
            return false;
        }
        try {

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveUserHouseOwner",
                type: "post",
                data: '{model:{UserName:"' + sUserName + '",Password:"' + sPassword + '",HouseOwnerId:"' + houseOwnerId + '"}}',
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
                        window.location.reload();
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
    },
    GetPswByRandom: function () {
        $.ajax({
            url: "/House/ScriptServices/AdminService.asmx/GetPasswordByRandom",
            type: "post",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#dlgWaiting").dialog('open');
            },
            complete: function () {
                $("#dlgWaiting").dialog('close');
            },
            success: function (data) {
                $("#txtPsw").val(data.d);
            }
        });
    }
} 