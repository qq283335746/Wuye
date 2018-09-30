
var DlgPictureSelect = {
    DlgOpenId: "",
    IsMutil: false,
    TableName: "",
    CallBack:"",
    DlgSingle: function (tableName) {
        this.TableName = tableName;
        this.DlgOpenId = "dlgSingleSelectPicture";
        this.IsMutil = false;
        $('#dlgSingleSelectPicture').dialog('open');
    },
    DlgMutil: function (tableName) {
        this.TableName = tableName;
        this.DlgOpenId = "dlgMutilSelectPicture";
        this.IsMutil = true;
        $('#dlgMutilSelectPicture').dialog('open');
    },
    SetSinglePicture: function (imgEleId) {
        var data = dlgSingleSelectPicture.GetPicSelect();
        if (data.length > 0) {
            var arr = data[0].split(",");
            $("#" + imgEleId + "").attr("src", arr[1])
            $("#" + imgEleId + "").parent().find("input[type=hidden]").val(arr[0]); 
            $("#dlgSingleSelectPicture").dialog('close');
        }
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
    },
    DlgUpload: function () {
        var tableName = this.TableName;
        var dlgParentId = this.DlgOpenId;
        var isMutil = this.IsMutil;
        var callBack = this.CallBack;
        if (tableName == "") {
            $.messager.alert('错误提醒', '未找到任何相册数据表，请检查', 'error');
            return false;
        }

        var h = $(window).height() * 0.8;
        $("#dlgUploadPicture").dialog({
            title: '上传文件',
            width: 606,
            height: h,
            closed: false,
            modal: true,
            href: '/House/t/ypicture.html?dlgId=dlgUploadPicture&funName=' + tableName + '&isMutil=' + isMutil + '&dlgParentId=' + dlgParentId + '&callBack=' + callBack + '&submitUrl=/h/tupload.html',
            buttons: [{
                id: 'btnUploadPicture', text: '上 传', iconCls: 'icon-ok',
                handler: function () {
                    dlgUploadPicture.OnUpload(); 
                }
            }, {
                id: 'btnCancelUploadPicture', text: '取 消', iconCls: 'icon-cancel',
                handler: function () {
                    $("#dlgUploadPicture").dialog('close');
                }
            }],
            toolbar: [{
                id: 'btnAddTextbox', text: '添 加', iconCls: 'icon-add',
                handler: function () {
                    dlgUploadPicture.OnToolbarAdd();
                }
            }]
        })
    }
}