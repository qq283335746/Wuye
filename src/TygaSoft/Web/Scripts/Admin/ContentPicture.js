
var ContentPicture = {
    IsSelectSingle: true,
    Init: function () {
        var parmsData = { reqName: "GetJsonForDatagrid", page: 1, rows: 10 };
        ContentPicture.BindPicture(parmsData);
    },
    BindPicture: function (parms) {
        $.get("/Handlers/Admin/HandlerContentPicture.ashx", parms, function (data) {
            var jsonData = eval("(" + data + ")");
            var ContentPictureBox = $("#ContentPictureBox");
            ContentPictureBox.children("div").filter(":not(:first)").remove();
            var row = ContentPictureBox.children("div").eq(0);
            $.map(jsonData.rows, function (item) {
                var newRow = row.clone(true);
                newRow.appendTo(ContentPictureBox);
                newRow.find("img").attr("src", item.MPicture);
                newRow.find("input[type=hidden]").val(item.Id);
                newRow.show();
            })

            if (ContentPicture.IsSelectSingle) {
                ContentPicture.PictureSingleClick();
            }
            else {
                ContentPicture.PictureMultiClick();
            }

            if (jsonData.total > 10) {
                $('#ContentPicturePager').pagination({
                    total: jsonData.total,
                    pageSize: 10,
                    onSelectPage: function (pageNumber, pageSize) {
                        var parmsData = { reqName: "GetJsonForDatagrid", page: pageNumber, rows: pageSize };
                        ContentPicture.BindPicture(parmsData);
                    }
                });
            }
        })
    },
    PictureSingleClick: function () {
        $("#ContentPictureBox").children().bind("click", function () {
            $(this).addClass("curr").siblings().removeClass("curr");
        })
    },
    PictureMultiClick: function () {
        $("#ContentPictureBox").children().on("click", function () {
            var fistThis = $(this);
            var picId = fistThis.find("input[type=hidden]").val();
            if (!fistThis.hasClass('curr')) {
                fistThis.addClass("curr");
                var hasSelectItem = false;
                $("#imgContentPicture").find("input[type=hidden]").each(function () {
                    if ($(this).val() == picId) hasSelectItem = true;
                })
                if (hasSelectItem) return false;

                var firstCol = $("#imgContentPicture").children().eq(0);
                var newCol = firstCol.clone(true);
                newCol.appendTo($("#imgContentPicture"));
                newCol.show();
                newCol.find("img").attr("src", fistThis.find("img").attr("src"));
                newCol.find("input[type=hidden]").val(fistThis.find("input[type=hidden]").val());

            }
            else {
                fistThis.removeClass("curr");
                $("#imgContentPicture").find("input[type=hidden]").each(function () {
                    if ($(this).val() == picId) {
                        $(this).parents(".row_col").remove();
                    }
                })
            }

        })
    },
    IsDlgContentPicture: false,
    DlgSingle: function () {
        ContentPicture.IsSelectSingle = true;
        $("#dlgContentPicture").dialog('open');
        if (!ContentPicture.IsDlgContentPicture) {
            ContentPicture.IsDlgContentPicture = true;
            ContentPicture.Init();
        }
    },
    DlgMulti: function () {
        ContentPicture.IsSelectSingle = false;
        $("#dlgContentPicture").dialog('open');
        if (!ContentPicture.IsDlgContentPicture) {
            ContentPicture.IsDlgContentPicture = true;
            ContentPicture.Init();
        }
    },
    OnAddFilebox: function () {
        $("#dlgUploadFm").attr("id", "dlgUploadFm_ContentPicture");
        var currDlgFm = $("#dlgUploadFm_ContentPicture");

        var rowLen = currDlgFm.children("div").length;
        rowLen++;
        var newRow = $("<div class=\"mb10\"><input type=\"text\" id=\"ContentPicture_file" + rowLen + "\" name=\"ContentPicture_file" + rowLen + "\" style=\"width:500px;\" /><a href=\"#\" onclick=\"$(this).parent().remove();return false;\" style=\"margin-left:10px;\">删 除</a></div>");
        currDlgFm.append(newRow);
        newRow.find("#ContentPicture_file" + rowLen + "").filebox({
            buttonText: '选择文件',
            prompt: '选择图片'
        })
        newRow.find("a:last").linkbutton({
            iconCls: 'icon-remove',
            plain: true
        });
    },
    DlgUpload: function () {
        $("#dlgUploadContentPicture").dialog('open');
        $('#dlgUploadFm').find('input').each(function () {
            $(this).attr("id", "ContentPicture_" + $(this).attr("id") + "");
        })
    },
    OnUpload: function () {
        try {
            $.messager.progress({
                title: '请稍等',
                msg: '正在执行...'
            });
            $('[id*=dlgUploadFm]').form('submit', {
                url: '/Handlers/Admin/HandlerContentPicture.ashx',
                onSubmit: function (param) {
                    var hasNotFile = true;
                    $('[id*=dlgUploadFm]').find("[class*=filebox-f]").each(function () {
                        if ($.trim($(this).filebox('getValue')) == "") {
                            hasNotFile = false;
                        }
                    })
                    if (!hasNotFile) {
                        $.messager.progress('close');
                        $.messager.alert('错误提示', "包含一个或多个未选择文件，无法上传，请检查！", 'error');
                        return false;
                    }
                    param.reqName = "OnUpload";

                    return hasNotFile;
                },
                success: function (data) {
                    $.messager.progress('close');
                    var jsonData = eval('(' + data + ')');
                    if (!jsonData.success) {
                        $.messager.alert('错误提示', jsonData.message, 'error');
                        return false;
                    }
                    jeasyuiFun.show("温馨提醒", jsonData.message);
                    $("#dlgUploadContentPicture").dialog('close');
                    if (ContentPicture.IsSelectSingle) {
                        ContentPicture.DlgSingle();
                    }
                    else {
                        ContentPicture.DlgMulti();
                    }
                }
            });
        }
        catch (e) {
            $.messager.progress('close');
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    FormatPicture: function (value, rowData, rowIndex) {
        if (value == undefined) return "";
        return "<img src=\"" + value + "\" alt=\"\" />";
    }
}