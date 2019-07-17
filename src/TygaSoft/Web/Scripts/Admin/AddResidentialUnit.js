
var AddResidentialUnit = {
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
                    companyId: rowData.Id
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
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                $('#dgBuilding').datagrid('options').url = "/House/Handlers/Admin/HandlerResidentialBuilding.ashx";
                $('#dgBuilding').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    communityId: rowData.Id
                });
                $('#tabParent').tabs('select', "楼");
            }
        })
    },
    CreateDgBuilding: function () {
        $("#dgBuilding").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'BuildingCode', title: '楼号', width: 100 }
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            singleSelect: true,
            fit: true,
            fitColumns: true,
            toolbar: '#dgBuildingToolbar',
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
                        $.map(AddResidentialUnit.GetMyData("myDataForModelInfo"), function (item) {
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
                AddResidentialUnit.GetTreeParent(t, pNode);
            }
        }
    },
    SetForm: function () {
        $.map(AddResidentialUnit.GetMyData("myDataForModelInfo"), function (item) {
            $("#lbtnSelect").find(".l-btn-text").text(item.CompanyName + "," + item.CommunityName + "," + item.BuildingCode);
            $("#hPropertyCompanyId").val(item.PropertyCompanyId);
            $("#hResidenceCommunityId").val(item.ResidenceCommunityId);
            $("#hResidentialBuildingId").val(item.ResidentialBuildingId);
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
    OnSearchBuilding: function () {
        $('#dgBuilding').datagrid('load', {
            buildingCode: $.trim($("#txtBuildingCode").val())
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

            AddResidentialUnit.CreateDgCompany();
        }
        if (!$('#tabParent').tabs('exists', '小区')) {
            $('#tabParent').tabs('add', {
                title: '小区',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgCommunity"></div>'
            });

            AddResidentialUnit.CreateDgCommunity();
        }
        if (!$('#tabParent').tabs('exists', '楼')) {
            $('#tabParent').tabs('add', {
                title: '楼',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgBuilding"></div>'
            });

            AddResidentialUnit.CreateDgBuilding();
        }

    },
    OnDlgLoad: function () {
        alert("OnDlgLoad");
    },
    OnDlgSelect: function () {

        var rowCompany = $("#dgCompany").datagrid('getSelected');
        if (!rowCompany) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
            return false;
        }
        var rowCommunity = $("#dgCommunity").datagrid('getSelected');
        if (!rowCommunity) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
            return false;
        }
        var rowBuilding = $("#dgBuilding").datagrid('getSelected');
        if (!rowBuilding) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
            return false;
        }
        $("#lbtnSelect").find(".l-btn-text").text(rowCompany.CompanyName + "," + rowCommunity.CommunityName + "," + rowBuilding.BuildingCode);
        $("#hPropertyCompanyId").val(rowCompany.Id);
        $("#hResidenceCommunityId").val(rowCommunity.Id);
        $("#hResidentialBuildingId").val(rowBuilding.Id);
        $('#dlg').dialog('close');
    },
    OnSelectPage: function () {
        alert("点击分页了");
    },
    OnSave: function () {
        try {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            var Id = "";
            $.map(AddResidentialUnit.GetMyData("myDataForModelInfo"), function (item) {
                Id = item.Id;
            })

            var propertyCompanyId = $("#hPropertyCompanyId").val();
            var residenceCommunityId = $("#hResidenceCommunityId").val();
            var residentialBuildingId = $("#hResidentialBuildingId").val();
            if (propertyCompanyId == "") {
                $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
                return false;
            }
            if (residenceCommunityId == "") {
                $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
                return false;
            }
            if (residentialBuildingId == "") {
                $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
                return false;
            }

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveResidentialUnit",
                type: "post",
                data: '{model:{Id:"' + Id + '",UnitCode:"' + $.trim($("#txtName").val()) + '",PropertyCompanyId:"' + propertyCompanyId + '",ResidenceCommunityId:"' + residenceCommunityId + '",ResidentialBuildingId:"' + residentialBuildingId + '",Remark:"' + $.trim($("#txtaRemark").val()) + '"}}',
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