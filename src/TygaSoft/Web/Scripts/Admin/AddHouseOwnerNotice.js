
var AddHouseOwnerNotice = {
    Init: function () {

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
            fit: true,
            fitColumns: true,
            onCheck: function (rowIndex, rowData) {
                if ($("#cbbType").combobox('getValue') < 1) {
                    var dgDataEmpty = { "total": 0, "rows": [] };
                    $("#dgCommunity").datagrid('loadData', dgDataEmpty);
                    $('#dgBuilding').datagrid('loadData', dgDataEmpty);
                    $('#dgUnit').datagrid('loadData', dgDataEmpty);
                    $('#dgHouse').datagrid('loadData', dgDataEmpty);
                    return false;
                }

                $('#dgCommunity').datagrid('options').url = "/House/Handlers/Admin/HandlerResidenceCommunity.ashx";
                $('#dgCommunity').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    companyId: rowData.Id
                });
                $('#tabHouseOwner').tabs('select', "小区");
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
            fit: true,
            fitColumns: true,
            toolbar: '#dgCommunityToolbar',
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                if ($("#cbbType").combobox('getValue') < 2) {
                    var dgDataEmpty = { "total": 0, "rows": [] };
                    $('#dgBuilding').datagrid('loadData', dgDataEmpty);
                    $('#dgUnit').datagrid('loadData', dgDataEmpty);
                    $('#dgHouse').datagrid('loadData', dgDataEmpty);
                    return false;
                }

                $('#dgBuilding').datagrid('options').url = "/House/Handlers/Admin/HandlerResidentialBuilding.ashx";
                $('#dgBuilding').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    communityId: rowData.Id
                });
                $('#tabHouseOwner').tabs('select', "楼");
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
            fit: true,
            fitColumns: true,
            toolbar: '#dgBuildingToolbar',
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                if ($("#cbbType").combobox('getValue') < 3) {
                    var dgDataEmpty = { "total": 0, "rows": [] };
                    $('#dgUnit').datagrid('loadData', dgDataEmpty);
                    $('#dgHouse').datagrid('loadData', dgDataEmpty);
                    return false;
                }
                $('#dgUnit').datagrid('options').url = "/House/Handlers/Admin/HandlerResidentialUnit.ashx";
                $('#dgUnit').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    buildingId: rowData.Id
                });
                $('#tabHouseOwner').tabs('select', "单元");
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
            fit: true,
            fitColumns: true,
            toolbar: '#dgUnitToolbar',
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                if ($("#cbbType").combobox('getValue') < 4) {
                    var dgDataEmpty = { "total": 0, "rows": [] };
                    $("#dgHouse").datagrid('loadData', dgDataEmpty);
                    return false;
                }
                $('#dgHouse').datagrid('options').url = "/House/Handlers/Admin/HandlerHouse.ashx";
                $('#dgHouse').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    unitId: rowData.Id
                });
                $('#tabHouseOwner').tabs('select', "房间");
            }
        })
    },
    CreateDgHouse: function () {
        $("#dgHouse").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'HouseCode', title: '房号', width: 100 }
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            fit: true,
            fitColumns: true,
            toolbar: '#dgHouseToolbar',
            method: 'GET',
            onCheck: function (rowIndex, rowData) {
                if ($("#cbbType").combobox('getValue') < 5) {
                    var dgDataEmpty = { "total": 0, "rows": [] };
                    $("#dgHouseOwner").datagrid('loadData', dgDataEmpty);
                    return false;
                }
                $('#dgHouseOwner').datagrid('options').url = "/House/Handlers/Admin/HandlerHouseOwner.ashx";
                $('#dgHouseOwner').datagrid('load', {
                    reqName: "GetJsonForDatagrid",
                    houseId: rowData.Id
                });
                $('#tabHouseOwner').tabs('select', "业主");
            }
        })
    },
    CreateDgHouseOwner: function () {
        $("#dgHouseOwner").datagrid({
            columns: [[
                    { field: 'Id', checkbox: true },
                    { field: 'HouseOwnerName', title: '业主名称'}
                ]],
            selected: true,
            border: false,
            rownumbers: true,
            pagination: true,
            fit: true,
            fitColumns: true,
            toolbar: '#dgHouseOwnerToolbar',
            method: 'GET'
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
    OnSearchHouse: function () {
        $('#dgHouse').datagrid('load', {
            houseCode: $.trim($("#txtHouseCode").val())
        });
    },
    OnDlgHouseOwner: function () {
        if ($('#cbbType').combobox('getValue') == -1) {
            $.messager.alert('错误提示', '请选择下发通知至', 'error');
            return false;
        }
        $('#dlgHouseOwner').dialog('open');
        var cbbSelectIndex = parseInt($('#cbbType').combobox('getValue'));
        if (cbbSelectIndex > -1) {
            var currTab = $('#tabHouseOwner').tabs('getSelected');
            var currIndex = $('#tabHouseOwner').tabs('getTabIndex', currTab);
            if (currIndex > cbbSelectIndex) {
                $("#tabHouseOwner").tabs('unselect', currIndex);
                $("#tabHouseOwner").tabs('select', currIndex - 1);
                var dgDataEmpty = { "total": 0, "rows": [] };
                switch (cbbSelectIndex) {
                    case 0:
                        $('#dgCommunity').datagrid('loadData', dgDataEmpty);
                        $('#dgBuilding').datagrid('loadData', dgDataEmpty);
                        $('#dgUnit').datagrid('loadData', dgDataEmpty);
                        $('#dgHouse').datagrid('loadData', dgDataEmpty);
                        break;
                    case 1:
                        $('#dgBuilding').datagrid('loadData', dgDataEmpty);
                        $('#dgUnit').datagrid('loadData', dgDataEmpty);
                        $('#dgHouse').datagrid('loadData', dgDataEmpty);
                        break;
                    case 2:
                        console.log("2");
                        $('#dgUnit').datagrid('loadData', dgDataEmpty);
                        $('#dgHouse').datagrid('loadData', dgDataEmpty);
                        break;
                    case 3:
                        $('#dgHouse').datagrid('loadData', dgDataEmpty);
                        break;
                    case 4:
                        $('#dgHouseOwner').datagrid('loadData', dgDataEmpty);
                        break;
                    default:
                        break;
                }
            }
        }
    },
    OnSearchHouseOwner: function () {
        $('#dgHouseOwner').datagrid('load', {
            houseOwnerName: $.trim($("#txtHouseOwnerName").val())
        });
    },
    OnDlgHouseOwnerOpen: function () {
        if (!$('#tabHouseOwner').tabs('exists', '物业公司')) {
            $('#tabHouseOwner').tabs('add', {
                title: '物业公司',
                style: { paddingTop: 10 },
                content: '<div id="dgCompany"></div>'
            });

            AddHouseOwnerNotice.CreateDgCompany();
        }
        if (!$('#tabHouseOwner').tabs('exists', '小区')) {
            $('#tabHouseOwner').tabs('add', {
                title: '小区',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgCommunity"></div>'
            });

            AddHouseOwnerNotice.CreateDgCommunity();
        }
        if (!$('#tabHouseOwner').tabs('exists', '楼')) {
            $('#tabHouseOwner').tabs('add', {
                title: '楼',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgBuilding"></div>'
            });

            AddHouseOwnerNotice.CreateDgBuilding();
        }
        if (!$('#tabHouseOwner').tabs('exists', '单元')) {
            $('#tabHouseOwner').tabs('add', {
                title: '单元',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgUnit"></div>'
            });

            AddHouseOwnerNotice.CreateDgUnit();
        }
        if (!$('#tabHouseOwner').tabs('exists', '房间')) {
            $('#tabHouseOwner').tabs('add', {
                title: '房间',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgHouse"></div>'
            });

            AddHouseOwnerNotice.CreateDgHouse();
        }
        if (!$('#tabHouseOwner').tabs('exists', '业主')) {
            $('#tabHouseOwner').tabs('add', {
                title: '业主',
                selected: false,
                style: { paddingTop: 10 },
                content: '<div id="dgHouseOwner"></div>'
            });

            AddHouseOwnerNotice.CreateDgHouseOwner();
        }
    },
    OnDlgHouseOwnerSelect: function () {
        var cbbTypeIndex = parseInt($("#cbbType").combobox('getValue'));
        switch (cbbTypeIndex) {
            case 0:
                var rows = $("#dgCompany").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择物业公司！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择物业公司");
                $('#dlgHouseOwner').dialog('close');
                break;
            case 1:
                var rows = $("#dgCommunity").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择小区！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择小区");
                $('#dlgHouseOwner').dialog('close');
                break;
            case 2:
                var rows = $("#dgBuilding").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择楼！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择楼");
                $('#dlgHouseOwner').dialog('close');
                break;
            case 3:
                var rows = $("#dgUnit").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择单元！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择单元");
                $('#dlgHouseOwner').dialog('close');
                break;
            case 4:
                var rows = $("#dgHouse").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择房间！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择房间");
                $('#dlgHouseOwner').dialog('close');
                break;
            case 5:
                var rows = $("#dgHouseOwner").datagrid("getSelections");
                if (!rows || rows.length == 0) {
                    $('#abtnHouseOwner').find(".l-btn-text").text("选 择");
                    $.messager.alert('错误提示', '请选择业主！', 'error');
                    return false;
                }
                $('#abtnHouseOwner').find(".l-btn-text").text("已选择业主");
                $('#dlgHouseOwner').dialog('close');
                break;
            default:
                break;
        }
    },
    OnDlgNotice: function () {
        $("#dlgNotice").dialog('open');
    },
    OnDlgNoticeSelect: function () {
        var rows = $("#dgNotice").datagrid("getSelections");
        if (!rows || rows.length == 0) {
            $('#abtnNotice').find(".l-btn-text").text("选 择");
            $.messager.alert('错误提示', '请选择通知！', 'error');
            return false;
        }
        $('#abtnNotice').find(".l-btn-text").text("已选择通知");
        $('#dlgNotice').dialog('close');
    },
    OnSearchNotice: function () {
        $('#dgNotice').datagrid('load', {
            req: 'GetJsonForNoticeDatagrid',
            title: $.trim($("#txtNoticeTitle").val())
        });
    },
    OnSave: function () {
        try {
            var selectTyle = parseInt($("#cbbType").combobox('getValue'));
            if (selectTyle == -1) {
                $.messager.alert('错误提示', '请选择下发通知至！', 'error');
                return false;
            }
            var ownerAppend = "";
            var noticeAppend = "";
            var errorMsg = "";
            switch (selectTyle) {
                case 0:
                    var rows = $("#dgCompany").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择物业公司";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }
                    break;
                case 1:
                    var rows = $("#dgCommunity").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择小区";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }

                    break;
                case 2:
                    var rows = $("#dgBuilding").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择楼";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }
                    
                    break;
                case 3:
                    var rows = $("#dgUnit").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择单元";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }
 
                    break;
                case 4:
                    var rows = $("#dgHouse").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择房间";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }

                    break;
                case 5:
                    var rows = $("#dgHouseOwner").datagrid('getSelections');
                    if (!rows || rows.length == 0) {
                        errorMsg = "请选择业主";
                        break;
                    }
                    for (var i = 0; i < rows.length; i++) {
                        ownerAppend += rows[i].Id + ",";
                    }
                    break;
                default:
                    break;
            }

            if (ownerAppend == "") {
                $.messager.alert('错误提示', errorMsg, 'error');
                return false;
            }

            var noticeRows = $("#dgNotice").datagrid('getSelections');
            if (!noticeRows || noticeRows.length == 0) {
                $.messager.alert('错误提示', "请选择通知", 'error');
                return false;
            }
            for (var j = 0; j < noticeRows.length; j++) {
                noticeAppend += noticeRows[j].Id + ",";
            }
            if (noticeAppend == "") {
                $.messager.alert('错误提示', "请选择通知", 'error');
                return false;
            }

            $.ajax({
                url: "/House/ScriptServices/AdminService.asmx/SaveHouseOwnerNotice",
                type: "post",
                data: '{model:{ReachType:"' + selectTyle + '", OwnerAppend:"' + ownerAppend + '",NoticeAppend:"' + noticeAppend + '"}}',
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
                        jeasyuiFun.show("温馨提示", "发送成功！");
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