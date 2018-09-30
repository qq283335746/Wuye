
var MenusFun = {
    Init: function () {
        MenusFun.SelectCurrent();
        MenusFun.Hover();
    },
    Hover: function () {
        $(".nav a").hover(function () {
            $(this).addClass("hover").siblings().removeClass("hover");
        }, function () {
            $(this).removeClass("hover")
        })
    },
    SelectCurrent: function () {
        var currMenu = $("#SitePaths>span:last").text();
        $(".nav a").filter(":contains('" + currMenu + "')").addClass("curr").siblings().removeClass("curr");
    }
};

var UserMenus = {
    Init: function () {
        UserMenus.TreeLoad();
    },
    TreeLoad: function () {
        var t = $("#eastTree");
        $.ajax({
            url: "/House/ScriptServices/UsersService.asmx/GetTreeJsonForMenu",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                var jsonData = (new Function("", "return " + json.d))();
                t.tree({
                    data: jsonData,
                    formatter: function (node) {
                        if (node.id.length > 0) {
                            return "<a href=\"" + node.id + "\">" + node.text + "</a>";
                        }
                        return node.text;
                    },
                    animate: true
                })
                UserMenus.SelectCurrent();
                //t.children().children("div:first").hide();
            }
        });
    },
    SelectCurrent: function () {
        var navArr = $("#eastTree").find("a");
        var spanArr = $("#SitePaths>span:not(:contains('>'))");
        var spanArrLen = spanArr.length - 1;
        for (i = spanArrLen; i >= 0; i--) {
            var hlNav = navArr.filter(":contains('" + spanArr.eq(i).text() + "')");
            console.log(hlNav.length);
            if (hlNav && hlNav.length > 0) {
                if (hlNav.length > 1) {
                    var winHref = window.location;
                    for (var j = 0; j < hlNav.length; j++) {
                        if (winHref.indexOf(hlNav[j].attr('href')) > -1) {
                            hlNav[j].parent().parent().addClass("bg_curr");
                            break;
                        }
                    }
                }
                else hlNav.parent().parent().addClass("bg_curr");
                break;
            }
        }
    }
};

var AdminMenus = {

    Init: function () {
        AdminMenus.TreeLoad();
    },
    TreeLoad: function () {
        var t = $("#eastTree");
        $.ajax({
            url: "/House/ScriptServices/AdminService.asmx/GetTreeJsonForMenu",
            type: "post",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                var jsonData = (new Function("", "return " + json.d))();
                t.tree({
                    data: jsonData,
                    formatter: function (node) {
                        if (node.id.length > 0) {
                            return "<a href=\"" + node.id + "\">" + node.text + "</a>";
                        }
                        return node.text;
                    },
                    animate: true
                })
                AdminMenus.SelectCurrent();
                //t.children().children("div:first").hide();
            }
        });
    },
    SelectCurrent: function () {
        var navArr = $("#eastTree").find("a");
        var spanArr = $("#SitePaths>span:not(:contains('>'))");
        var spanArrLen = spanArr.length - 1;
        for (i = spanArrLen; i >= 0; i--) {
            var hlNav = navArr.filter(":contains('" + spanArr.eq(i).text() + "')");
            if (hlNav && hlNav.length > 0) {
                if (hlNav.length > 1) {
                    var winHref = window.location.href;
                    hlNav.each(function () {
                        if (winHref.indexOf($(this).attr('href')) > -1) {
                            $(this).parent().parent().addClass("bg_curr");
                            return false;
                        }
                    })
                }
                else hlNav.parent().parent().addClass("bg_curr");
                break;
            }
        }
    }
};

var Common = {
    AppName: '/House'
};