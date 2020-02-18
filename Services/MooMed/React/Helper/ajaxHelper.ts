import * as $ from "jquery";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "./popUpMessageHelper";

interface IAjaxParams {
    actionUrl: string;
    data?: any;
    onSuccess?: (data: any) => any;
    onError?: (data: any) => any;
    useVerificationToken?: boolean;
    uploadFile?: boolean;
}

export default function ajaxPost(ajaxParams: IAjaxParams): void {

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
            error: (data: any) => {
                if (ajaxParams.onError) {
                    ajaxParams.onError(data.responseJSON);
                } else {
                    defaultErrorHandler(data.errors);
                }
            }
        });
    } else {
        $.ajax({
            url: ajaxParams.actionUrl,
            method: "POST",
            data: ajaxParams.data,
            dataType: "json",
            success: ajaxParams.onSuccess,
            error: (data: any) => {
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


function attachVerificationToken(ajaxParams: IAjaxParams) {
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
            }
        }
    }
}

function defaultErrorHandler(data: any) {
    createPopUpMessage(data, PopUpMessageLevel.Error, "Request Error");
}