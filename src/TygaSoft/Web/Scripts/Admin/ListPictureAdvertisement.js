
var ListPictureAdvertisement = {
    Init: function () {
        this.Grid(sPageIndex, sPageSize);
        this.SetForm();
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
        window.location = "?startDate=" + $("#txtStartDate").datebox('getValue') + "&endDate=" + $("#txtEndDate").datebox('getValue') + "";
    },
    SetForm: function () {
        var myData = this.GetMyData("myDataForReq");
        $.map(myData, function (item) {
            if (item.startDate != "null") {
                $("#txtStartDate").datebox('setValue', item.startDate);
            }
            if (item.endDate != "null") {
                $("#txtEndDate").datebox('setValue', item.endDate);
            }
        })
    },
    Add: function () {
        DlgPictureSelect.TableName = "PictureAdvertisement";
        DlgPictureSelect.CallBack = "ListPictureAdvertisement.OnUploadCallBack()";
        DlgPictureSelect.DlgUpload();
    },
    OnUploadCallBack:function(){
        setTimeout(function () {
            window.location.reload();
        }, 1000);
    },
    Edit: function () {
        var cbl = $('#dgT').datagrid("getSelections");
        if (cbl && cbl.length == 1) {
            //window.location = "ay.html?Id=" + cbl[0].f0 + "";
        }
        else {
            $.messager.alert('错误提醒', '请选择一行且仅一行进行编辑', 'error');
        }
        return false;
    },
    Del: function () {
        try {
            var rows = $('#dgT').datagrid("getSelections");
            if (!rows || rows.length == 0) {
                $.messager.alert('错误提醒', '请至少选择一行再进行操作', 'error');
                return false;
            }
            var itemAppend = "";
            for (var i = 0; i < rows.length; i++) {
                if (i > 0) itemAppend += ",";
                itemAppend += rows[i].f0;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/DelPictureAdvertisement",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: '{itemAppend:"' + itemAppend + '"}',
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
                            setTimeout(function () {
                                window.location.reload();
                            }, 1000);
                        }
                    });
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    ViewPic: function (curr) {
        var $_this = $(curr);
        var h = $(window).height() * 0.9;
        var imgh = h - 70;
        $("#dlgViewPic").dialog({
            title: '查看原图',
            width: 780,
            height: 500,
            content: "<img src=\"" + $_this.attr("code") + "\" alt=\"\" width=\"746px\" height=\"444px\" />"
        })
    }
}