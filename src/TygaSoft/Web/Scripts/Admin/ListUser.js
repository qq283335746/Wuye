
var ListUser = {
    Init: function () {
        this.Grid(sPageIndex, sPageSize);
    },
    GetMyData: function (obj) {
        var myData = obj.html();
        return eval("(" + myData + ")");
    },
    Grid: function (pageIndex, pageSize) {
        var pager = $('#bindT').datagrid('getPager');
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
        window.location = "?name=" + $("[id$=txtUserName]").val() + "";
    },
    OnIsLockedOut: function (h) {
        try {
            var currObj = $(h);
            if (currObj.text() == "正常") {
                return false;
            }
            $.messager.confirm('温馨提醒', '确定要执行此操作吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/SaveIsLockedOut",
                        type: "post",
                        data: "{userName:'" + currObj.attr("code") + "'}",
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            $("#dlgWaiting").dialog('open');
                        },
                        complete: function () {
                            $("#dlgWaiting").dialog('close');
                        },
                        success: function (msg) {
                            msg = msg.d;
                            if (msg == "1") {
                                currObj.text("已锁定");
                                jeasyuiFun.show("温馨提醒", "操作成功");
                            }
                            else if (msg == "0") {
                                currObj.text("正常");
                                jeasyuiFun.show("温馨提醒", "操作成功");
                            }
                            else {
                                $.messager.alert('错误提醒', msg, 'error');
                            }
                        }
                    });
                }
            })
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    OnIsApproved: function (h) {
        try {
            var currObj = $(h);
            $.messager.confirm('温馨提醒', '确定要执行此操作吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/SaveIsApproved",
                        type: "post",
                        data: "{userName:'" + currObj.attr("code") + "'}",
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            $("#dlgWaiting").dialog('open');
                        },
                        complete: function () {
                            $("#dlgWaiting").dialog('close');
                        },
                        success: function (msg) {
                            msg = msg.d;
                            if (msg == "1") {
                                currObj.text("启用");
                                jeasyuiFun.show("温馨提醒", "操作成功");
                            }
                            else if (msg == "0") {
                                currObj.text("禁用");
                                jeasyuiFun.show("温馨提醒", "操作成功");
                            }
                            else {
                                $.messager.alert('错误提醒', msg, 'error');
                            }
                        }
                    });
                }
            })
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    Add: function () {
        window.location = "ty.html";
    },
    Edit: function () {

    },
    Del: function () {
        try {
            var cbl = $('#bindT').datagrid("getSelections");
            if (!cbl || cbl.length != 1) {
                $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
                return false;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    var userName = cbl[0].f0;
                    $.ajax({
                        url: "/House/ScriptServices/AdminService.asmx/DelUser",
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: '{userName:"' + userName + '"}',
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
    Save: function () {

    },
    SaveRole: function () {

    },
    EditRole: function (userName) {
        $("#lbUserName").text(userName);
        this.BindRole();
    },
    BindRole: function () {
        $("#dgRole").datagrid('loadData', ListUser.GetMyData($("#myDataForRole")));
    },
    RoleFormatter: function (value, row, index) {
        var isInRole = row.IsInRole == "True";
        if (isInRole) {
            return "<input type=\"checkbox\" checked=\"checked\" value=\"" + value + "\" onclick=\"ListUser.CbIsInRole(this)\" />" + value;
        }
        return "<input type=\"checkbox\" value=\"" + value + "\" onclick=\"ListUser.CbIsInRole(this)\" />" + value;
    },
    CbIsInRole: function (h) {
        try {
            var $_obj = $(h);
            var userName = $.trim($("#lbUserName").text());
            var roleName = $_obj.val();
            var isInRole = $_obj.is(":checked");

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveUserInRole",
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: '{userName:"' + userName + '",roleName:"' + roleName + '",isInRole:"' + isInRole + '"}',
                contentType: "application/json; charset=utf-8",
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
                    }
                }
            });
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    OnRoleLoadSuccess: function (data) {
        try {
            var userName = $("#lbUserName").text();
            var dg = $('#dgRole');
            var rows = dg.datagrid('getRows');
            if (rows && rows.length > 0) {
                $.ajax({
                    url: "/House/ScriptServices/AdminService.asmx/GetUserInRole",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    data: '{userName:"' + userName + '"}',
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $("#dlgWaiting").dialog('open');
                    },
                    complete: function () {
                        $("#dlgWaiting").dialog('close');
                    },
                    success: function (data) {
                        var roles = data.d;
                        if (roles.indexOf("异常") > -1) {
                            $.messager.alert('系统提示', msg, 'info');
                        }
                        if (roles.length > 0) {
                            for (var i = 0; i < rows.length; i++) {
                                if (roles.indexOf(rows[i].RoleName) > -1) {
                                    dg.datagrid('updateRow', {
                                        index: i,
                                        row: {
                                            IsInRole: 'True'
                                        }
                                    });
                                }
                            }
                        }
                    }
                });
            }
        }
        catch (e) {
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    AddUserHouseOwner: function () {
        var cbl = $('#bindT').datagrid("getSelections");
        if (!cbl || cbl.length != 1) {
            $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
            return false;
        }
        window.location = Common.AppName + "/a/ata.html?userName="+cbl[0].f1+"";
    },
    ResetPassword: function () {
        var rows = $('#bindT').datagrid("getSelections");
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提醒', '请选择一行且仅一行数据进行操作', 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要重置该用户的密码吗？', function (r) {
            if (r) {
                var username = rows[0].f0;

                $.ajax({
                    url: "/House/ScriptServices/AdminService.asmx/ResetPassword",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    data: '{username:"' + username + '"}',
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {
                        var jsonData = eval("(" + result.d + ")");
                        if (jsonData.ResCode != "1000") {
                            $.messager.alert('系统提示', jsonData.Message, 'info');
                            return false;
                        }
                        $.messager.alert('温馨提示', "新密码：" + jsonData.Data + "", 'info');
                    }
                });
            }
        });
    }
}