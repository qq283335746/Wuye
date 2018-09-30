
var AddAdvertisement = {
    Init: function () {
        //this.CbbContentType("txtPositionId");
        AddAdvertisement.DdlActionTypeId();
        this.SetForm();
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    DdlActionTypeId: function () {
        var json = AddAdvertisement.GetMyData("myDataForActionType");
        var s = "<option value=\"-1\">请选择</option>";
        $.map(json, function (item) {
            s += "<option value=\"" + item.id + "\">" + item.text + "</option>";
        })
        $("[name=ddlActionTypeId]").append(s);
        $("[name=ddlActionTypeId]").change(function () {
            AddAdvertisement.DisplayEle($(this).find("option:selected").text(), $(this));
        })
    },
    ActionTypeSelect: function (record) {
        AddAdvertisement.DisplayEle(record.text);
    },
    CbbContentType: function (clientId) {
        var cbt = $("#" + clientId + "");
        var parentId = $.trim(cbt.combotree("getValue"));
        var t = cbt.combotree('tree');
        $.ajax({
            url: "/House/ScriptServices/AdminService.asmx/GetJsonForContentTypeByTypeCode",
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: '{typeCode:"Advertisement"}',
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
    SetForm: function () {
        if ($("#hId").val() != "") {
            var myDataJson = AddAdvertisement.GetMyData("myDataForModel");
            $.map(myDataJson, function (item) {
                if (item.SiteFunId != "") {
                    $("#cbbSiteFun").combobox('setValue', item.SiteFunId);
                }
                if (item.LayoutPositionId != "") {
                    $("#cbbLayoutPosition").combobox('setValue', item.LayoutPositionId);
                }
                if (item.IsDisable != "") {
                    $("#cbbIsDisable").combobox('setValue', item.IsDisable == "True" ? "1" : "0");
                }
            })
            $("#imgContentPicture>div:not(:first)").each(function () {
                var currBox = $(this);
                var ddlActionTypeId = currBox.find("select[name=ddlActionTypeId]");
                var actionTypeId = currBox.find("[name=ActionTypeId]");
                ddlActionTypeId.find("option").each(function () {
                    if ($(this).attr("value") == actionTypeId.val()) {
                        ddlActionTypeId.val(actionTypeId.val());
                        AddAdvertisement.DisplayEle($.trim($(this).text()), ddlActionTypeId);
                    }
                })
                var cbIsDisable = currBox.find("[name=IsDisable]");
                if (cbIsDisable.val() == "True") {
                    cbIsDisable.attr("checked", "checked");
                }
            })
        }
    },
    DisplayEle: function (text, t) {
        var currParent = t.parents("table");
        switch (text) {
            case "跳转至外部网页":
                currParent.find("[name=Url]").parent().parent().show();
                break;
            case "图文":
                currParent.find("[name=Url]").parent().parent().hide();
                break;
            default:
                currParent.find("[name=Url]").parent().parent().hide();
                break;
        }
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

                    param.reqName = "SaveAdvertisement";
                    param.content = editor_content.html().replace(/</g, "&lt;");
                    param.siteFunId = $("#cbbSiteFun").combobox('getValue');
                    param.layoutPositionId = $("#cbbLayoutPosition").combobox('getValue');

                    var adLinkJson = "";
                    var index = 0;
                    var error = "";
                    $("#imgContentPicture>div:not(:first)").each(function () {
                        var sAdLinkId = $(this).find("input[name=AdLinkId]").val();
                        var sContentPictureId = $.trim($(this).find("input[name=PicId]").val());
                        if (sContentPictureId == "") {
                            error = "包含有未选择图片的框，请检查";
                            return;
                        }
                        var sActionTypeId = $(this).find("[name=ddlActionTypeId]").val();
                        var sActionTypeText = $.trim($(this).find("[name=ddlActionTypeId]").find("option:selected").text());
                        if (sActionTypeId == "-1") {
                            $.messager.progress('close');
                            error = "包含有未选择图片的框，请检查";
                            return;
                        }
                        var sUrl = $.trim($(this).find("input[name=Url]").val());
                        var sSort = $.trim($(this).find("input[name=Sort]").val());
                        if (sActionTypeText == "图文" || sActionTypeText == "无跳转") sUrl = "";
                        if (sSort != "") {
                            var reg = /(\d+)/;
                            if (!reg.test(sSort)) {
                                error = "排序为整数字符，请检查";
                                return;
                            }
                        }
                        if (sSort == "") sSort = 0;
                        var isDisable = $(this).find("input[name=IsDisable]")[0].checked;
                        if (index > 0) adLinkJson += ",";
                        adLinkJson += "{\"AdLinkId\":\"" + sAdLinkId + "\",\"ContentPictureId\":\"" + sContentPictureId + "\",\"ActionTypeId\":\"" + sActionTypeId + "\",\"Url\":\"" + sUrl + "\",\"Sort\":\"" + sSort + "\",\"IsDisable\":\"" + isDisable + "\"}";

                        index++;
                    })
                    if (error != "") {
                        $.messager.progress('close');
                        $.messager.alert('错误提示', error, 'error');
                        return false;
                    }
                    param.adLinkJson = "[" + adLinkJson + "]";

                    if ($("#hId").val() != "") {
                        $("#txtContent").text("");
                    }

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
                }
            });
        }
        catch (e) {
            $.messager.progress('close');
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    DlgProduct: function () {
        $("#dlgSelectProduct").dialog('open');
    },
    CbtCategory: function (id) {
        $.get('/Handlers/Admin/ECShop/HandlerCategory.ashx', { reqName: 'GetCategoryTreeJson' }, function (data) {
            data = eval("(" + data + ")");
            $("#" + id + "").combotree('loadData', data);
        })
    },
    CbtBrand: function (id) {
        $.get('/Handlers/Admin/ECShop/HandlerBrand.ashx', { reqName: 'GetBrandTreeJson' }, function (data) {
            data = eval("(" + data + ")");
            $("#" + id + "").combotree('loadData', data);
        })
    },
    SearchDgProduct: function () {
        $('#dlgDgProduct').datagrid('load', {
            reqName: 'GetJsonForDatagrid',
            keyword: $.trim($('#txtKeyword').val()),
            categoryId: $('#cbtCategory').combotree('getValue'),
            brandId: $('#cbtBrand').combotree('getValue')
        });
    },
    FImageUrl: function (value, row, index) {
        if (value == undefined) {
            return "";
        }
        return "<img src=\"../.." + value + "\" alt=\"\" />";
    },
    FPictureAppend: function (value, row, index) {
        if (value == undefined) {
            return "";
        }
        var picArr = value.split(",");
        var s = "";
        for (var i = 0; i < 5; i++) {
            s += "<img src=\"../.." + picArr[i] + "\" alt=\"\" />"
        }
        if (picArr.length > 5) s += "...";
        return s;
    },
    SelectProduct: function () {
        var rows = $('#dlgDgProduct').datagrid("getSelections");
        var abtnSelectProduct = $("#abtnSelectProduct");
        if (!(rows && rows.length == 1)) {
            abtnSelectProduct.find(".l-btn-text").text("选择商品");
            abtnSelectProduct.parent().find("input[type=hidden]").val("");
            $.messager.alert('错误提醒', '未选择任何商品', 'error');
            return false;
        }
        abtnSelectProduct.find(".l-btn-text").text("已选择商品");
        abtnSelectProduct.parent().find("input[type=hidden]").val(rows[0].Id);
    },
    CbbActionTypeData: function () {
        return AddAdvertisement.GetMyData("myDataForActionType");
    },
    SetMutilPicture: function (imgEleId) {
        var data = dlgMutilSelectPicture.GetPicSelect();
        if (data.length > 0) {
            var imgEle = $("#" + imgEleId + "");
            var firstCol = imgEle.children().eq(0);
            for (var i = 0; i < data.length; i++) {
                var arr = data[i].split(",");
                var hasExist = false;
                imgEle.find("input[type=hidden]").each(function () {
                    if ($(this).val() == arr[0]) {
                        hasExist = true;
                        return false;
                    }
                })
                if (!hasExist) {

                    var newCol = firstCol.clone(true);
                    newCol.appendTo(imgEle);
                    newCol.find("img").attr("src", arr[1]);
                    newCol.find("input[type=hidden]").val(arr[0]);
                    newCol.show();
                }
            }
        }
    }
} 