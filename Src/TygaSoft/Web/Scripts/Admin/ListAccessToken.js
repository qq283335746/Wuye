
var ListAccessToken = {
    Init: function () {
        ListAccessToken.Grid(sPageIndex, sPageSize);
    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    Grid: function (pageIndex, pageSize) {
        var pager = $('#dgT').datagrid('getPager');
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
    }
}