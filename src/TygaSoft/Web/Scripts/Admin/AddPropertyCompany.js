var AddPropertyCompany = {
    Init: function () {
        this.CbtProvinceCity("cbtProvinceCity");
        this.SetForm();
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    CbtProvinceCity: function (clientId) {
        var cbt = $("#" + clientId + "");
        var t = cbt.combotree('tree');

        $.ajax({
            url: "/House/ScriptServices/AdminService.asmx/GetJsonForProvinceCity",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                var jsonData = (new Function("", "return " + json.d))();
                t.tree({
                    data: jsonData,
                    animate: true,
                    onLoadSuccess: function () {
                        var provinceCityId = "";
                        $.map(AddPropertyCompany.GetMyData("myDataForModelInfo"), function (item) {
                            provinceCityId = item.ProvinceCityId;
                        })
                        var node = t.tree('find', provinceCityId);
                        if (node) {
                            cbt.combotree('setValue', provinceCityId);
                        }
                    }
                })
            }
        });
    },
    GetTreeParent: function (t, node) {
        var sAppend = $("#hTreeParentAppend").val();
        if (node) {
            if (sAppend != "") {
                sAppend = "," + sAppend;
            }
            sAppend = node.text + sAppend;
            $("#hTreeParentAppend").val(sAppend);
            var pNode = t.tree('getParent', node.target);
            if (pNode) {
                AddPropertyCompany.GetTreeParent(t, pNode);
            }
        }
    },
    SetForm: function () {
        $.map(AddPropertyCompany.GetMyData("myDataForModelInfo"), function (item) {
            $("#txtCompanyName").val(item.CompanyName);
            $("#txtShortName").val(item.ShortName);
        })
    },
    OnSave: function () {
        try {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            var sProvinceCityAppend = "";
            var sProvinceCityId = "";
            var sProvinceCityText = "";
            var sProvince = "";
            var sCity = "";
            var sDistrict = "";

            var cbtProvinceCity = $('#cbtProvinceCity');
            var t = cbtProvinceCity.combotree('tree');
            var currNode = t.tree('getSelected');
            if (!currNode) {
                $.messager.alert('错误提示', '请选择省市区', 'error');
                return false;
            }
            sProvinceCityId = currNode.id;
            sProvinceCityText = currNode.text;
            AddPropertyCompany.GetTreeParent(t, currNode);
            sProvinceCityAppend = $("#hTreeParentAppend").val();
            var arr = new Array();
            arr = sProvinceCityAppend.split(",");
            for (i = 0; i < arr.length; i++) {
                switch (i) {
                    case 1:
                        sProvince = arr[i];
                        break;
                    case 2:
                        sCity = arr[i];
                        break;
                    case 3:
                        sDistrict = arr[i];
                        break;
                    default:
                        break;
                }
            }

            var Id = "";
            $.map(AddPropertyCompany.GetMyData("myDataForModelInfo"), function (item) {
                Id = item.Id;
            })

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SavePropertyCompany",
                type: "post",
                data: '{model:{Id:"' + Id + '",CompanyName:"' + $.trim($("#txtCompanyName").val()) + '",ShortName:"' + $.trim($("#txtShortName").val()) + '",ProvinceCityId:"' + sProvinceCityId + '",Province:"' + sProvince + '",City:"' + sCity + '",District:"' + sDistrict + '"}}',
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
    }


} 