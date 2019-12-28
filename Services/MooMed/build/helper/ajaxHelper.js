"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("./popUpMessageHelper");
function ajaxPost(ajaxParams) {
    attachVerificationToken(ajaxParams);
    if (ajaxParams.uploadFile) {
        $.ajax({
            url: ajaxParams.actionUrl,
            method: "POST",
            data: ajaxParams.data,
            processData: false,
            contentType: false,
            dataType: "json",
            success: ajaxParams.onSuccess,
            error: (data) => {
                if (ajaxParams.onError) {
                    ajaxParams.onError(data.responseJSON);
                }
                else {
                    defaultErrorHandler(data.errors);
                }
            }
        });
    }
    else {
        $.ajax({
            url: ajaxParams.actionUrl,
            method: "POST",
            data: ajaxParams.data,
            dataType: "json",
            success: ajaxParams.onSuccess,
            error: (data) => {
                if (ajaxParams.onError) {
                    ajaxParams.onError(data.responseJSON);
                }
                else {
                    defaultErrorHandler(data.errors);
                }
            }
        });
    }
}
exports.default = ajaxPost;
function attachVerificationToken(ajaxParams) {
    if (ajaxParams.useVerificationToken === undefined) {
        ajaxParams.useVerificationToken = true;
    }
    if (ajaxParams.useVerificationToken) {
        if (ajaxParams.data !== undefined) {
            ajaxParams.data.__RequestVerificationToken = $("input[name='__RequestVerificationToken']", $("#__AjaxAntiForgeryToken")).val();
        }
        else {
            ajaxParams.data = {
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']", $("#__AjaxAntiForgeryToken")).val()
            };
        }
    }
}
function defaultErrorHandler(data) {
    popUpMessageHelper_1.createPopUpMessage(data, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, "Request Error");
}
//# sourceMappingURL=ajaxHelper.js.map