/*******************************************************
场合：绑定枚举
参数：
    id:控件ID或者类选择器，如：#StoreType/.StoreType
    value:默认值
    isAll:是否可用所有（如果为true则会多一项“请选择”）

模板编写：深圳乾海盛世（Jie）
更新人员：PC201605031109
更新时间：2016年08月26日 18时15分37秒
注意：此文件是由T4模板生成，如需要更改，请更改模板！！！
*******************************************************/
var apiEnum = {};

/*
说明：绑定 RunStatus 枚举到下拉框
*/
apiEnum.bindRunStatus = function (options) {
    var settings = {
        id: "#RunStatus",
        url: "/Enum/RunStatus/"
    };
    settings = $.extend(settings, options);
    bindComboBox(settings);
}

/*
说明：绑定 Cycle 枚举到下拉框
*/
apiEnum.bindCycle = function (options) {
    var settings = {
        id: "#Cycle",
        url: "/Enum/Cycle/"
    };
    settings = $.extend(settings, options);
    bindComboBox(settings);
}

/*
说明：绑定 IsSend 枚举到下拉框
*/
apiEnum.bindIsSend = function (options) {
    var settings = {
        id: "#IsSend",
        url: "/Enum/IsSend/"
    };
    settings = $.extend(settings, options);
    bindComboBox(settings);
}

/*
说明：绑定 SendType 枚举到下拉框
*/
apiEnum.bindSendType = function (options) {
    var settings = {
        id: "#SendType",
        url: "/Enum/SendType/"
    };
    settings = $.extend(settings, options);
    bindComboBox(settings);
}


//绑定下拉框
function bindComboBox(options) {
    var settings = {
        isAll: false,
        required: false,
        editable: false,
        panelHeight: 120,
        valueField: 'Value',
        textField: 'Text',
    };
    settings = $.extend(settings, options);
    if (settings.isAll) {
        settings.url = settings.url + "?all=1";
    }
    $(settings.id).combobox(settings);
}