var ListAdvertisement = {
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
    FProductPic: function (value, row, index) {
        if ($.trim(row.ActionTypeName) == "跳转至商品") {
            return "<img src=\"../.." + value + "\" alt=\"\" width=\"50px\" height=\"50px\" /> <br />" + row.ProductName + "";
        }
        return "";
    },
    ReloadGrid: function () {
        var currUrl = "";
        if (sQueryStr.length == 0) {
            currUrl = "?pageIndex=" + sPageIndex + "&pageSize=" + sPageSize + "";
        }
        else {
            currUrl = "?" + sQueryStr + "&pageIndex=" + sPageIndex + "&pageSize=" + sPageSize + "";
        }
        window.location = currUrl;
    },
    Search: function () {
        window.location = "?keyword=" + $.trim($("[id$=txtName]").val()) + "";
    },
    Add: function () {
        window.location = "tga.html";
    },
    Edit: function () {

        var cbl = $('#dgT').datagrid("getSelections");
        if (!(cbl && cbl.length == 1)) {
            $.messager.alert('错误提醒', '请选择一行且仅一行进行编辑', 'error');
            return false;
        }
        window.location = "tga.html?Id=" + cbl[0].f0;
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
                        url: "/House/ScriptServices/AdminService.asmx/DelAdvertisement",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: '{itemAppend:"' + itemsAppend + '"}',
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
                            ListAdvertisement.ReloadGrid();
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