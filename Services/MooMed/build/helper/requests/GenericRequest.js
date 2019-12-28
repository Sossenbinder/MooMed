"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
class GenericRequest {
    constructor(url) {
        this.m_url = url;
    }
    SetUrl(url) {
        this.m_url = url;
    }
    send(requestData) {
        return __awaiter(this, void 0, void 0, function* () {
            requestData["__RequestVerificationToken"] = $("input[name='__RequestVerificationToken']", $("#__AjaxAntiForgeryToken")).val();
            const params = Object.keys(requestData).map((key) => {
                return encodeURIComponent(key) + '=' + encodeURIComponent(requestData[key]);
            }).join('&');
            return fetch(this.m_url, {
                method: this.RequestType,
                body: params,
                headers: {
                    'Accept': 'application/json, text/javascript, */*',
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                credentials: 'include'
            }).then((response) => __awaiter(this, void 0, void 0, function* () {
                const responseJson = response.ok ? yield response.json() : undefined;
                return {
                    success: response.ok,
                    errorMessage: response.statusText,
                    statusCode: response.status,
                    payload: responseJson,
                };
            }));
        });
    }
}
exports.default = GenericRequest;
//# sourceMappingURL=GenericRequest.js.map