

var AddHouse = {
    Init: function () {
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
                    { field: 'PropertyCompanyId', hidden: true },
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
                    companyId: rowData.PropertyCompanyId,
                    communityId: rowData.Id,
                    buildingCode: $.trim($("#txtBuildingCode").val())
                });
                $('#tabParent').tabs('select', "楼");
            }
        })
    },
    CreateDgBuilding: function () {
        $("#dgBuilding").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'PropertyCompanyId', hidden: true },
                    { field: 'ResidenceCommunityId', hidden: true },
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
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                $('#dgUnit').datagrid('options').url = "/House/Handlers/Admin/HandlerResidentialUnit.ashx";
                $('#dgUnit').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    companyId: rowData.PropertyCompanyId,
                    communityId: rowData.ResidenceCommunityId,
                    buildingId: rowData.Id,
                    unitCode: $.trim($("#txtUnitCode").val())
                });
                $('#tabParent').tabs('select', "单元");
            }
        })
    },
    CreateDgUnit: function () {
        $("#dgUnit").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'UnitCode', title: '单元号', width: 100 }
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            singleSelect: true,
            fit: true,
            fitColumns: true,
            toolbar: '#dgUnitToolbar',
            method: 'GET'
        })
    },
    SetForm: function () {
        $.map(AddHouse.GetMyData("myDataForModelInfo"), function (item) {
            $("#lbtnSelect").find(".l-btn-text").text(item.CompanyName + "," + item.CommunityName + "," + item.BuildingCode + "," + item.UnitCode);
            $("#hPropertyCompanyId").val(item.PropertyCompanyId);
            $("#hResidenceCommunityId").val(item.ResidenceCommunityId);
            $("#hResidentialBuildingId").val(item.ResidentialBuildingId);
            $("#hResidentialUnitId").val(item.ResidentialUnitId);
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
    OnSearchUnit: function () {
        $('#dgUnit').datagrid('load', {
            unitCode: $.trim($("#txtUnitCode").val())
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

            AddHouse.CreateDgCompany();
        }
        if (!$('#tabParent').tabs('exists', '小区')) {
            $('#tabParent').tabs('add', {
                title: '小区',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgCommunity"></div>'
            });

            AddHouse.CreateDgCommunity();
        }
        if (!$('#tabParent').tabs('exists', '楼')) {
            $('#tabParent').tabs('add', {
                title: '楼',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgBuilding"></div>'
            });

            AddHouse.CreateDgBuilding();
        }
        if (!$('#tabParent').tabs('exists', '单元')) {
            $('#tabParent').tabs('add', {
                title: '单元',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgUnit"></div>'
            });

            AddHouse.CreateDgUnit();
        }

    },
    OnDlgLoad: function () {
        alert("OnDlgLoad");
    },
    OnDlgSelect: function () {

        var rowCompany = $("#dgCompany").datagrid('getSelected');
        if (!rowCompany) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼，单元', 'error');
            return false;
        }
        var rowCommunity = $("#dgCommunity").datagrid('getSelected');
        if (!rowCommunity) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼，单元', 'error');
            return false;
        }
        var rowBuilding = $("#dgBuilding").datagrid('getSelected');
        if (!rowBuilding) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼，单元', 'error');
            return false;
        }
        var rowUnit = $("#dgUnit").datagrid('getSelected');
        if (!rowUnit) {
            $.messager.alert('错误提示', '请选择物业公司，小区，楼，单元', 'error');
            return false;
        }
        $("#lbtnSelect").find(".l-btn-text").text(rowCompany.CompanyName + "," + rowCommunity.CommunityName + "," + rowBuilding.BuildingCode + "," + rowUnit.UnitCode);
        $("#hPropertyCompanyId").val(rowCompany.Id);
        $("#hResidenceCommunityId").val(rowCommunity.Id);
        $("#hResidentialBuildingId").val(rowBuilding.Id);
        $("#hResidentialUnitId").val(rowUnit.Id);
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
            $.map(AddHouse.GetMyData("myDataForModelInfo"), function (item) {
                Id = item.Id;
            })

            var propertyCompanyId = $("#hPropertyCompanyId").val();
            var residenceCommunityId = $("#hResidenceCommunityId").val();
            var residentialBuildingId = $("#hResidentialBuildingId").val();
            var residentialUnitId = $("#hResidentialUnitId").val();
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
            if (residentialUnitId == "") {
                $.messager.alert('错误提示', '请选择物业公司，小区，楼', 'error');
                return false;
            }
            var sHouseAcreage = 0;
            if ($.trim($("#txtHouseAcreage").val()) != "") sHouseAcreage = $.trim($("#txtHouseAcreage").val());

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveHouse",
                type: "post",
                data: '{model:{Id:"' + Id + '",HouseCode:"' + $.trim($("#txtName").val()) + '",PropertyCompanyId:"' + propertyCompanyId + '",ResidenceCommunityId:"' + residenceCommunityId + '",ResidentialBuildingId:"' + residentialBuildingId + '",ResidentialUnitId:"' + residentialUnitId + '",HouseAcreage:"' + sHouseAcreage + '",Remark:"' + $.trim($("#txtaRemark").val()) + '"}}',
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