
var ListContentPicture = {
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
        var h = $(window).height() * 0.9;
        $("#dlgUpload").dialog({
            title: '上传文件',
            width: 700,
            height: h,
            closed: false,
            href: '/House/Templates/UploadPicture.htm',
            modal: true,
            buttons: [{
                text: '上 传',
                iconCls: 'icon-ok',
                handler: function () {
                    ListContentPicture.OnUpload();
                }
            }, {
                text: '取 消',
                iconCls: 'icon-cancel',
                handler: function () {
                    $("#dlgUpload").dialog('close');
                }
            }],
            toolbar: [{
                text: '添 加',
                iconCls: 'icon-add',
                handler: function () {
                    var rowLen = $("#dlgUploadFm").children("div").length;
                    rowLen++;
                    var newRow = $("<div class=\"mb10\"><input type=\"text\" id=\"file" + rowLen + "\" name=\"file" + rowLen + "\" style=\"width:500px;\" /><a href=\"#\" onclick=\"$(this).parent().remove();return false;\" style=\"margin-left:10px;\">删 除</a></div>");
                    $("#dlgUploadFm").append(newRow);
                    newRow.find("#file" + rowLen + "").filebox({
                        buttonText: '选择文件',
                        prompt: '选择图片'
                    })
                    newRow.find("a:last").linkbutton({
                        iconCls: 'icon-remove',
                        plain: true
                    });
                }
            }]
        })
    },
    OnUpload: function () {
        try {
            $.messager.progress({
                title: '请稍等',
                msg: '正在执行...'
            });
            $('#dlgUploadFm').form('submit', {
                url: '/House/Handlers/Admin/HandlerContentPicture.ashx',
                onSubmit: function (param) {
                    var isValid = $(this).form('validate');
                    if (!isValid) {
                        $.messager.progress('close');
                    }
                    param.reqName = "OnUpload";

                    return isValid;
                },
                success: function (data) {
                    $.messager.progress('close');
                    var jsonData = eval('(' + data + ')');
                    if (!jsonData.success) {
                        $.messager.alert('错误提示', jsonData.message, 'error');
                        return false;
                    }
                    jeasyuiFun.show("温馨提醒", jsonData.message);

                    setTimeout(function () {
                        window.location = "yyt.html";
                    }, 1000);
                }
            });
        }
        catch (e) {
            $.messager.progress('close');
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
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
                        url: "/House/ScriptServices/AdminService.asmx/DelContentPicture",
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