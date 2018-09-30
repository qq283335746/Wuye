

var AddNotice = {
    Init: function () {
        this.CbbContentType("txtParent");
    },
    CbbContentType: function (clientId) {
        var cbt = $("#" + clientId + "");
        var parentId = $.trim(cbt.combotree("getValue"));
        var t = cbt.combotree('tree');
        $.ajax({
            url: "/House/ScriptServices/AdminService.asmx/GetJsonForContentTypeByTypeCode",
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: '{typeCode:"Notice"}',
            dataType: "json",
            success: function (data) {
                var jsonData = (new Function("", "return " + data.d))();
                cbt.combotree('loadData', jsonData);
                if (parentId != "") {
                    var node = t.tree("find", parentId);
                    if (node) {
                        t.tree('select', node.target);
                        cbt.combotree("setValue", node.text);
                    }
                }
                else {
                    var root = t.tree('getRoot');
                    if (root) {
                        t.tree('select', root.target);
                        cbt.combotree("setValue", root.text);
                    }
                }
            }
        });
    },
    OnSave: function () {
        try {
            $.messager.progress({
                title: '请稍等',
                msg: '正在执行...'
            });
            $('#form1').form('submit', {
                url: '/House/Handlers/Admin/HandlerAnnouncement.ashx',
                onSubmit: function (param) {
                    var isValid = $(this).form('validate');
                    if (!isValid) {
                        $.messager.progress('close'); 
                    }

                    param.reqName = "SaveNotice";
                    param.txtContent = editor_content.html().replace(/</g, "&lt;");
                    var contentTypeId = "";
                    var t = $("#txtParent").combotree('tree');
                    var node = t.tree("getSelected");
                    if (node) {
                        contentTypeId = node.id;
                    }
                    param.contentTypeId = contentTypeId;
                    if ($("#hId").val() != "") {
                        $("#txtContent").text("");
                    }
                    return isValid;
                },
                success: function (data) {
                    $.messager.progress('close');
                    var data = eval('(' + data + ')');
                    if (!data.success) {
                        $.messager.alert('错误提示', data.message, 'error');
                        return false;
                    }
                    jeasyuiFun.show("温馨提醒", data.message);
                    setTimeout(5000, window.location = "tyt.html");
                }
            });
        }
        catch (e) {
            $.messager.progress('close');
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    }
} 