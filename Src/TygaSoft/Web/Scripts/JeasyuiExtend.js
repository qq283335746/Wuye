$.extend($.fn.validatebox.defaults.rules, {
    cfmPsw: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '前后输入密码不正确，请检查'
    },
    psw: {
        validator: function (value, param) {
            var reg = /(([0-9]+)|([a-zA-Z]+)){6,30}/;
            return reg.test(value);
        },
        message: '密码正确格式由数字或字母组成的字符串，且最小6位，最大30位'
    },
    phone: {
        validator: function (value, param) {
            return /^(\d+){11,15}$/.test(value);
        },
        message: '请正确输入手机号或电话号码'
    },
    telPhone: {
        validator: function (value, param) {
            return /^(\d+){8,15}$/.test(value);
        },
        message: '电话号码正确格式为数字'
    },
    QQ: {
        validator: function (value, param) {
            return /^(\d+){5,15}$/.test(value);
        },
        message: '请输入正确的QQ号码'
    },
    percentage: {
        validator: function (value, param) {
            return /^(\d{1,2})(0)?$|^((\d{1,2})\.(\d{1,2}))$/.test($(param[0]).val());
        },
        message: '请输入正确的百分比数值，保留两位小数'
    },
    price: {
        validator: function (value, param) {
            return /(^\d+$)|(^(\d+)\.(\d+){1,2}$)/.test(value);
        },
        message: '请输入正确的金额数'
    },
    int: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    },
    float: {
        validator: function (value, param) {
            return /^\d+(\.\d+)?$/.test(value);
        },
        message: '请输入数字或浮点数'
    },
    numberlength: {
        validator: function (value, param) {
            if (value.length !== 15) {
                return true;
            } else {
                return false;
            }
        },
        message: '请输入15位的数字的IMEI号！'
    },
    rate: {
        validator: function (value, param) {
            if (/^\d+$/.test(value)) {
                return value >= 0 && value <= 100;
            } else {
                return false;
            }
        },
        message: '请输入0至100的数字.'
    },
    ratebase: {
        validator: function (value, param) {
            return value >= 0 && value <= 1;
        },
        message: '请输入0至1的之间的值.'
    },
    dateMaxCompare: {
        validator: function (value, param) {
            return Date.parse(value) >= Date.parse($(param[0]).datebox('getValue'));
        },
        message: '开始时间不能大于结束时间'
    },
    haschinese: {
        validator: function (value, param) {
            return !(/[^\x00-\xff]/g.test(value));
        },
        message: '不能包含中文'
    },
    numberAndEnglish: {
        validator: function (value, param) {
            return !(/[^\w\.\/]/.test(value));
        },
        message: '只能是数字和英文'
    },
    ticketNum: {
        validator: function (value, param) {
            return /(\d+){3,4}/.test(value);
        },
        message: '请输入3数或4数的数字'
    }
});
