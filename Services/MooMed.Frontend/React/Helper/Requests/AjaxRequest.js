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
var RequestMethods;
(function (RequestMethods) {
    RequestMethods["GET"] = "GET";
    RequestMethods["POST"] = "POST";
})(RequestMethods = exports.RequestMethods || (exports.RequestMethods = {}));
class AjaxRequest {
    constructor(url, requestMethod = RequestMethods.POST) {
        this.m_url = url;
        this.m_requestMethod = requestMethod;
    }
    send(requestData, verificationToken) {
        return __awaiter(this, void 0, void 0, function* () {
            const requestInit = {
                method: this.m_requestMethod,
                cache: "no-cache",
                headers: {
                    'Accept': 'application/json, text/javascript, */*',
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            };
            if (verificationToken) {
                requestInit.headers["AntiForgery"] = verificationToken;
            }
            if (this.m_requestMethod === RequestMethods.POST && typeof requestData !== "undefined") {
                const params = Object
                    .keys(requestData)
                    .map((key) => encodeURIComponent(key) + '=' + encodeURIComponent(requestData[key]))
                    .join('&');
                requestInit.body = JSON.stringify(requestData);
            }
            const response = yield fetch(this.m_url, requestInit);
            const jsonResponse = response.ok ? yield response.json() : undefined;
            const payload = typeof jsonResponse !== "undefined" ? jsonResponse === null || jsonResponse === void 0 ? void 0 : jsonResponse.data : undefined;
            return {
                success: jsonResponse.success,
                payload,
            };
        });
    }
}
exports.default = AjaxRequest;
//# sourceMappingURL=AjaxRequest.js.map