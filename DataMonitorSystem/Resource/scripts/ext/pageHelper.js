/// <reference path="../jquery/jquery-1.8.3.min.js" />
/// <reference path="../jquery-easyui/jquery.easyui.min.js" />
/// <reference path="FrameHelper.js" />

var PageHelper = {}

PageHelper.GetFrameHelper = function () {
    return window.top.FrameHelper;
}

PageHelper.TabPage =
{
    Open: function (options) {
        PageHelper.GetFrameHelper().TabPage.Open(options);
    },
    Close: function (title) {
        var options = { title: title };
        PageHelper.GetFrameHelper().TabPage.Close(options);
    },
    Callback: function (title, result) {
        var options = { title: title, result: result };
        PageHelper.GetFrameHelper().TabPage.Callback(options);
    }
}

PageHelper.Window =
{
    Open: function (options) {
        PageHelper.GetFrameHelper().Window.Open(options);
    },
    Close: function () {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Window.Close({ iframeId: iframeId });
        }
    },
    Callback: function (result) {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Window.Callback({ iframeId: iframeId, result: result });
        }
    }
}

PageHelper.Dialog =
{
    Open: function (options) {
        PageHelper.GetFrameHelper().Dialog.Open(options);
    },
    Close: function () {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Dialog.Close({ iframeId: iframeId });
        }
    },
    Callback: function (result) {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Dialog.Callback({ iframeId: iframeId, result: result });
        }
    },
    DisableButton: function (disabled) {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Dialog.DisableButton({ iframeId: iframeId, disabled: disabled });
        }
    },
    Submit: function (fn) {
        if (window.frameElement) {
            var iframeId = $(window.frameElement).attr("id");
            PageHelper.GetFrameHelper().Dialog.Submit({ iframeId: iframeId, fn: fn });
        }
    }

}

PageHelper.Message =
{
    ShowError: function (title, msg) {
        msg = msg.replace("\};", "};<br/>");
        PageHelper.GetFrameHelper().Message.ShowError(title, msg);
    },
    ShowInfo: function (title, msg) {
        PageHelper.GetFrameHelper().Message.ShowInfo(title, msg);
    },
    ShowWarning: function (title, msg) {
        PageHelper.GetFrameHelper().Message.ShowWarning(title, msg);
    },
    ShowConfirm: function (title, msg, fn) {
        PageHelper.GetFrameHelper().Message.ShowConfirm(title, msg, fn);
    },
    ShowPopu: function (title, msg, timeout) {
        PageHelper.GetFrameHelper().Message.ShowPopu(title, msg);
    }
}