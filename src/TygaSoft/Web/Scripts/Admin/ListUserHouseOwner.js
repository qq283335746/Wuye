

var ListUserHouseOwner = {
    Init: function () {
        this.Grid(sPageIndex, sPageSize);
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
        window.location = "?userName=" + $.trim($("#lbUser").text()) + "&name=" + $.trim($("[id$=txtName]").val()) + "";
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
            $.messager.confirm('温馨提醒', '确定要取消绑定吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/DelUserHouseOwner",
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
    AddUserHouseOwner: function () {
        var h = $(window).height() * 0.9;
        if (h > 450) h = 450;
        $('#dlgUserHouseOwner').dialog({
            title: '业主列表',
            width: 780,
            height: h,
            closed: false,
            cache: false,
            iconCls: 'icon-man',
            modal: true,
            buttons: [{
                text: '确定',
                iconCls: 'icon-ok',
                handler: function () {
                    if (parseInt($("#hSuccessCount").val()) > 0) {
                        window.location.reload();
                    }
                    $('#dlgUserHouseOwner').dialog('close');
                }
            }],
            onOpen: function () {
                var rows = $("#dgHouseOwner").datagrid('getRows');
                if (rows > 0) return;
                //$("#dgHouseOwner").datagrid('options').url = "/h/t.html?reqName=GetJsonForDatagrid";
                //$("#dgHouseOwner").datagrid('reload');
            }
        });
    },
    DgHouseOwnerSearch: function () {
        $('#dgHouseOwner').datagrid('load', {
            houseOwnerName: $.trim($("#txtHouseOwnerName").val())
        });
    },
    DgHouseOwnerOnCheck: function (rowIndex, rowData) {
        var userName = $.trim($("#lbUser").text());
        $.ajax({
            url: "/House/Handlers/Admin/HandlerHouseOwner.ashx",
            type: "post",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: { reqName: "AddUserHouseOwner", houseOwnerId: rowData.Id, userName: userName },
            beforeSend: function () {
                $("#dlgWaiting").dialog('open');
            },
            complete: function () {
                $("#dlgWaiting").dialog('close');
            },
            success: function (data) {
                var jsonData = eval("(" + data + ")");
                if (!jsonData.success) {
                    $.messager.alert('系统提示', jsonData.message, 'info');
                    return false;
                }
                if (jsonData.message.indexOf('已存在') == -1) {
                    $("#hSuccessCount").val(parseInt($("#hSuccessCount").val()) + 1);
                }
            }
        });
    },
    DgHouseOwnerOnUncheck: function (rowIndex, rowData) {
        var userName = $.trim($("#lbUser").text());
        $.ajax({
            url: "/House/Handlers/Admin/HandlerHouseOwner.ashx",
            type: "post",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: { reqName: "DelUserHouseOwner", houseOwnerId: rowData.Id, userName: userName },
            beforeSend: function () {
                $("#dlgWaiting").dialog('open');
            },
            complete: function () {
                $("#dlgWaiting").dialog('close');
            },
            success: function (data) {
                var jsonData = eval("(" + data + ")");
                if (!jsonData.success) {
                    $.messager.alert('系统提示', jsonData.message, 'info');
                    return false;
                }

                $("#hSuccessCount").val(parseInt($("#hSuccessCount").val()) - 1);
            }
        });
    }
} 