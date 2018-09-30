

var AddResidentialBuilding = {
    Init: function () {
        //this.CbtProvinceCity("cbtProvinceCity");
        this.SetForm();
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    CreateDgCompany: function () {
        $("#dgCompany").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'CompanyName', title: '公司名称', width: 120 },
                    { field: 'ShortName', title: '公司简称', width: 100 },
                    { field: 'Province', title: '省', width: 80 },
                    { field: 'City', title: '市', width: 80 },
                    { field: 'District', title: '区', width: 80 }
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            singleSelect: true,
            fit: true,
            fitColumns: true,
            onCheck: function (rowIndex, rowData) {
                $('#dgCommunity').datagrid('options').url = "/House/Handlers/Admin/HandlerResidenceCommunity.ashx";
                $('#dgCommunity').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    companyId: rowData.Id,
                    communityName: $.trim($("#txtCommunityName").val())
                });
                $('#tabParent').tabs('select', "小区");
            },
            toolbar: '#dgCompanyToolbar',
            method: 'GET',
            url: '/House/Handlers/Admin/HandlerPropertyCompany.ashx?reqName=GetJsonForDatagrid'
        })
    },
    CreateDgCommunity: function () {
        $("#dgCommunity").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'CommunityName', title: '小区名称', width: 100 },
                    { field: 'Address', title: '地址', width: 100 }
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            singleSelect: true,
            fit: true,
            fitColumns: true,
            toolbar: '#dgCommunityToolbar',
            method: 'GET'
        })
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
                        $.map(AddResidentialBuilding.GetMyData("myDataForModelInfo"), function (item) {
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
                AddResidentialBuilding.GetTreeParent(t, pNode);
            }
        }
    },
    SetForm: function () {
        $.map(AddResidentialBuilding.GetMyData("myDataForModelInfo"), function (item) {
            $("#lbtnSelect").find(".l-btn-text").text(item.CompanyName + "," + item.CommunityName);
            $("#hPropertyCompanyId").val(item.PropertyCompanyId);
            $("#hResidenceCommunityId").val(item.ResidenceCommunityId);
        })
    },
    OnSearchCompany: function () {
        $('#dgCompany').datagrid('load', {
            companyName: $.trim($("#txtCompanyName").val())
        });
    },
    OnSearchCommunity: function () {
        $('#dgCommunity').datagrid('load', {
            communityName: $.trim($("#txtCommunityName").val())
        });
    },
    OnDlgOpenClick: function () {
        $('#dlg').dialog('open');
    },
    OnDlgOpen: function () {
        if (!$('#tabParent').tabs('exists', '物业公司')) {
            $('#tabParent').tabs('add', {
                title: '物业公司',
                style: { paddingTop: 10 },
                content: '<div id="dgCompany"></div>'
            });

            AddResidentialBuilding.CreateDgCompany();
        }
        if (!$('#tabParent').tabs('exists', '小区')) {
            $('#tabParent').tabs('add', {
                title: '小区',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgCommunity"></div>'
            });

            AddResidentialBuilding.CreateDgCommunity();
        }

    },
    OnDlgLoad: function () {
        alert("OnDlgLoad");
    },
    OnDlgSelect: function () {

        var row = $("#dgCompany").datagrid('getSelected');
        if (!row) {
            $.messager.alert('错误提示', '请选择物业公司和小区', 'error');
            return false;
        }
        var rowCommunity = $("#dgCommunity").datagrid('getSelected');
        if (!rowCommunity) {
            $.messager.alert('错误提示', '请选择物业公司和小区', 'error');
            return false;
        }
        $("#lbtnSelect").find(".l-btn-text").text(row.CompanyName + "," + rowCommunity.CommunityName);
        $("#hPropertyCompanyId").val(row.Id);
        $("#hResidenceCommunityId").val(rowCommunity.Id);
        $('#dlg').dialog('close');
    },
    OnSelectPage: function () {
        alert("点击分页了");
    },
    OnSave: function () {
        try {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            var sCoveredArea = $.trim($("#txtCoveredArea").val());
            var coveredArea = 0;
            if (sCoveredArea != "") coveredArea = parseFloat(sCoveredArea);
            var Id = "";
            $.map(AddResidentialBuilding.GetMyData("myDataForModelInfo"), function (item) {
                Id = item.Id;
            })

            var propertyCompanyId = $("#hPropertyCompanyId").val();
            var residenceCommunityId = $("#hResidenceCommunityId").val();
            if (propertyCompanyId == "") {
                $.messager.alert('错误提示', '请选择物业公司和小区', 'error');
                return false;
            }
            if (residenceCommunityId == "") {
                $.messager.alert('错误提示', '请选择物业公司和小区', 'error');
                return false;
            }

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveResidentialBuilding",
                type: "post",
                data: '{model:{Id:"' + Id + '",BuildingCode:"' + $.trim($("#txtName").val()) + '",CoveredArea:"' + coveredArea + '",PropertyCompanyId:"' + propertyCompanyId + '",ResidenceCommunityId:"' + residenceCommunityId + '",Remark:"' + $.trim($("#txtaRemark").val()) + '"}}',
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