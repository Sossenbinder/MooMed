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
class GetRequest {
    constructor(url) {
        this.m_url = url;
    }
    send() {
        return __awaiter(this, void 0, void 0, function* () {
            return fetch(this.m_url, {
                method: "GET",
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
exports.default = GetRequest;
//# sourceMappingURL=GetRequest.js.map