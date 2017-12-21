/// <reference path="../jquery/jquery-1.10.2.min.js" />
/// <reference path="../jquery-easyui/jquery.easyui.min.js" />

var FrameHelper = {}

FrameHelper.TabPage =
{
    createIFrame: function(iframeID, iframeUrl)
    {
        var $iframeWrap = $("<div id=\"" + iframeID + "Wrap\" style=\"position:relative;height:100%\"></div>");
        var $iframeLoad = $("<div class=\"" + iframeID + "Load\" style=\"position:absolute;top:10px;left:10px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在加载...</div>");
        var $iframe = $("<iframe class=\"" + iframeID + "\" src=\"" + iframeUrl + "\" style=\"width:100%;height:100%;margin:auto;\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\"></iframe>");

        $iframeWrap.append($iframeLoad);
        $iframeWrap.append($iframe);
        $iframe.load(function () {
            $iframeLoad.hide();
            $iframeLoad.remove();
        });

        return $iframeWrap;
    },
    Open: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名",
            url: "about:blank",
            iconCls: "glyphicon glyphicon-star-empty",
            closable: true,
            callback: null
        };

        settings = $.extend(settings, options);

        var $tab = $("#" + settings.tabpanelID);

        if ($tab.tabs("exists", settings.title)) {
            $tab.tabs("select", settings.title);
            return;
        }

        var $iframeWrap = FrameHelper.TabPage.createIFrame(settings.title, settings.url);
        var $iframeLoad = $("." + settings.title + "Load", $iframeWrap);
        var $iframe = $("." + settings.title, $iframeWrap);

        $tab.tabs("add",
        {
            title: settings.title,
            closable: settings.closable,
            iconCls: settings.iconCls,
            content: $iframeWrap
        });

        //$.data($iframe[0], "callback", settings.callback);
    },
    Close: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名"
        };

        settings = $.extend(settings, options);

        var $tab = $("#" + settings.tabpanelID);
        $tab.tabs("close", settings.title);
    },
    Callback: function (options) {
        var settings = {
            tabpanelID: "tabpanel",
            title: "未命名",
            result: null
        };

        settings = $.extend(settings, options);

        var $iframeWrap = $("#" + settings.title + "Wrap");
        var $iframe = $("." + settings.title, $iframeWrap);

        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(options.result); }
    }
}

FrameHelper.Window =
{
    createIFrame: function (iframeID) {
        var $iframeWrap = $("<div id=\"" + iframeID + "Wrap\" style=\"position:relative;height:100%\"></div>");
        var $iframeLoad = $("<div id=\"" + iframeID + "Load\" style=\"position:absolute;top:10px;left:10px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在加载...</div>");
        var $iframe = $("<iframe id=\"" + iframeID + "Frame\" src=\"about:blank\" style=\"width:98%;height:98%;\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\"></iframe>");

        $iframeWrap.append($iframeLoad);
        $iframeWrap.append($iframe);
        $iframe.load(function () {
            $iframeLoad.hide();
            $iframeLoad.remove();
        });

        return $iframeWrap;
    },
    Open: function (options) {
        var settings = {
            title: "未命名",
            url: "about:blank",
            width: 600,
            height: 400,
            iconCls: null,
            collapsible: true,
            minimizable: false,
            maximizable: true,
            closable: true,
            resizable: true,
            modal: true,
            callback: null
        };

        settings = $.extend(settings, options);

        //设置WindowID
        var $randomId = Math.floor(Math.random() * 999 + 1);
        var $iframeWrap = FrameHelper.Window.createIFrame("window_" + $randomId);
        var $iframeLoad = $("#window_" + $randomId + "Load", $iframeWrap);
        var $iframe = $("#window_" + $randomId + "Frame", $iframeWrap);
        $.data($iframe[0], "randomId", $randomId);
        $.data($iframe[0], "callback", settings.callback);

        var $window = $("<div id=\"window_" + $randomId + "\"></div>");
        $("body").append($window);

        //创建Window对象
        $window.window({
            title: settings.title,
            width: settings.width,
            height: settings.height,
            iconCls: settings.iconCls,
            minimizable: settings.minimizable,
            maximizable: settings.maximizable,
            closable: settings.closable,
            resizable: settings.resizable,
            modal: settings.modal,
            content: $iframeWrap,
            onClose: function () {
                $.removeData($iframe[0], "randomId");
                $.removeData($iframe[0], "callback");

                $iframe[0].contentWindow.document.write('');
                $iframe[0].contentWindow.close();
                $iframe.attr("src", "about:blank");
                $iframe.remove();

                $window.window("destroy");
                $window.remove();
            }
        });

        $iframe.attr("src", options.url);
        $window.window("open");
    },
    Close: function (options) {
        var settings = {
            iframeId: "0"
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var $randomId = $.data($iframe[0], "randomId");
        var $window = $("#window_" + $randomId);
        $window.window("close");
    },
    Callback: function (options) {
        var settings = {
            iframeId: "0",
            result: null
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(settings.result); }
    }
}

FrameHelper.Dialog =
{
    createIFrame: function (iframeID) {
        var $iframeWrap = $("<div id=\"" + iframeID + "Wrap\" style=\"position:relative;height:100%\"></div>");
        var $iframeLoad = $("<div id=\"" + iframeID + "Load\" style=\"position:absolute;top:10px;left:10px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在加载...</div>");
        var $iframe = $("<iframe id=\"" + iframeID + "Frame\" src=\"about:blank\" style=\"width:98%;height:98%;\" frameborder=\"0\" marginwidth=\"0\" marginheight=\"0\"></iframe>");

        $iframeWrap.append($iframeLoad);
        $iframeWrap.append($iframe);
        $iframe.load(function () {
            $iframeLoad.hide();
            $iframeLoad.remove();
        });

        return $iframeWrap;
    },
    createButtons: function (buttonsID) {
        var $buttonsGroup = $("<div id=\"" + buttonsID + "Group\"></div>");
        var $buttonsOk = $("<a id=\"" + buttonsID + "Ok\" href=\"javascript:void(0)\"></a>");
        var $buttonsWait = $("<span id=\"" + buttonsID + "Wait\" style=\"display:none;margin-right:7px;\"><i class=\"fonticon-spin fonticon-refresh\"></i> 正在处理...</span>");
        var $buttonsCancel = $("<a id=\"" + buttonsID + "Cancel\" href=\"javascript:void(0)\"></a>");

        $buttonsGroup.append($buttonsOk);
        $buttonsGroup.append($buttonsWait);
        $buttonsGroup.append($buttonsCancel);

        return $buttonsGroup;
    },
    Open: function (options) {
        var settings = {
            title: "未命名",
            url: "about:blank",
            width: 600,
            height: 400,
            iconCls: null,
            collapsible: true,
            minimizable: false,
            maximizable: true,
            closable: true,
            resizable: true,
            modal: true,
            callback: null
        };

        settings = $.extend(settings, options);

        //设置对话框ID
        var $randomId = Math.floor(Math.random() * 999 + 1);
        var $iframeWrap = FrameHelper.Dialog.createIFrame("dlg_" + $randomId);
        var $iframeLoad = $("#dlg_" + $randomId + "Load", $iframeWrap);
        var $iframe = $("#dlg_" + $randomId + "Frame", $iframeWrap);
        var $buttonsGroup = FrameHelper.Dialog.createButtons("btn_" + $randomId);
        var $buttonsOk = $("#btn_" + $randomId + "Ok", $buttonsGroup);
        var $buttonsCancel = $("#btn_" + $randomId + "Cancel", $buttonsGroup);

        var $dlg = $("<div id=\"dlg_" + $randomId + "\"></div>");
        $("body").append($dlg);
        $("body").append($buttonsGroup);

        $.data($iframe[0], "randomId", $randomId);
        $.data($iframe[0], "callback", settings.callback);

        $buttonsOk.linkbutton({
            iconCls: 'glyphicon glyphicon-ok',
            text: "确定"
        })
        .click(function () {
            var _submit = $.data($iframe[0], "submit");
            if (_submit) { _submit(); }
        });

        $buttonsCancel.linkbutton({
            iconCls: 'glyphicon glyphicon-remove',
            text: "取消"
        })
        .click(function () {
            $dlg.dialog("close");
        });

        //创建对话框对象
        $dlg.dialog({
            title: settings.title,
            width: settings.width,
            height: settings.height,
            iconCls: settings.iconCls,
            collapsible: settings.collapsible,
            minimizable: settings.minimizable,
            maximizable: settings.maximizable,
            closable: settings.closable,
            resizable: settings.resizable,
            modal: settings.modal,
            buttons: "#btn_" + $randomId + "Group",
            content: $iframeWrap,
            onClose: function () {
                $.removeData($iframe[0], "randomId");
                $.removeData($iframe[0], "callback");

                $iframe.attr("src", "about:blank");
                $iframe[0].contentWindow.document.write('');
                $iframe[0].contentWindow.close();
                $iframe.remove();

                $dlg.dialog("destroy");
                $dlg.remove();

                $buttonsGroup.remove();
            }
        });

        $iframe.attr("src", options.url);

        $dlg.dialog("open");
    },
    Close: function (options) {
        var settings = {
            iframeId: "0"
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var $randomId = $.data($iframe[0], "randomId");
        var $dlg = $("#dlg_" + $randomId);

        $dlg.dialog("close");
    },
    Callback: function (options) {
        var settings = {
            iframeId: "0",
            result: null
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var callback = $.data($iframe[0], "callback");
        if (callback) { callback(settings.result); }
    },
    DisableButton: function (options) {
        var settings = {
            iframeId: "0",
            disabled: true
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        var $randomId = $.data($iframe[0], "randomId");

        var $buttonsGroup = $("#btn_" + $randomId + "Group");
        var $buttonsOk = $("#btn_" + $randomId + "Ok", $buttonsGroup);
        var $buttonsWait = $("#btn_" + $randomId + "Wait", $buttonsGroup);
        var $buttonsCancel = $("#btn_" + $randomId + "Cancel", $buttonsGroup);

        if (settings.disabled) {
            $buttonsOk.hide();
            $buttonsWait.show();
        }
        else {
            $buttonsOk.show();
            $buttonsWait.hide();
        }
    },
    Submit: function (options) {
        var settings = {
            iframeId: "0",
            fn: null
        };

        settings = $.extend(settings, options);

        var $iframe = $("#" + settings.iframeId);
        $.data($iframe[0], "submit", settings.fn);
    }
}

FrameHelper.Message =
{
    ShowError: function (title, msg) {
        $.messager.alert(title, msg, "error");
    },
    ShowInfo: function (title, msg) {
        $.messager.alert(title, msg, "info");
    },
    ShowWarning: function (title, msg) {
        $.messager.alert(title, msg, "warning");
    },
    ShowConfirm: function (title, msg, fn) {
        $.messager.confirm(title, msg, fn);
    },
    ShowPopu: function (title, msg, timeout) {
        $.messager.show({ title: title, msg: msg, timeout: timeout, showType: 'slide' });
    }
}