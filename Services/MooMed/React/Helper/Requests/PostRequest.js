"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
class PostRequest {
    constructor(url) {
        this.m_url = url;
    }
    send(requestData, attachVerificationToken = true) {
        return __awaiter(this, void 0, void 0, function* () {
            let params = undefined;
            if (attachVerificationToken) {
                if (requestData === undefined) {
                    requestData = Object.assign({});
                }
                requestData["__RequestVerificationToken"] = $("input[name='__RequestVerificationToken']", $("#__AjaxAntiForgeryToken")).val();
            }
            if (requestData !== undefined) {
                params = Object.keys(requestData).map((key) => {
                    return encodeURIComponent(key) + '=' + encodeURIComponent(requestData[key]);
                }).join('&');
            }
            return fetch(this.m_url, {
                method: "POST",
                body: params,
                headers: {
                    'Accept': 'application/json, text/javascript, */*',
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                credentials: 'include'
            }).then((response) => __awaiter(this, void 0, void 0, function* () {
                const responseJson = yield response.json();
                const responseJsonData = responseJson.data;
                return {
                    success: response.ok,
                    errorMessage: responseJsonData.errorMessage !== undefined ? responseJsonData.errorMessage : response.statusText,
                    statusCode: response.status,
                    payload: responseJsonData.data !== "undefined" ? responseJsonData.data : responseJsonData,
                };
            }));
        });
    }
}
exports.default = PostRequest;
//# sourceMappingURL=PostRequest.js.map